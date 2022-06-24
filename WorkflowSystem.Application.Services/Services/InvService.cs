using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkflowSystem.Data;
using TextClustering;
using WorkflowSystem.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NickStats;
using MathNet.Numerics.Statistics;

namespace WorkflowSystem.Application
{
    public class InvService : ServiceBase, IInvService
    {
        private readonly IInvRepository _invRepository;
        private readonly IInvPositionRepository _invPositionRepository;
        private readonly IUserRoleService _userRoleService;
        private readonly IClassificationService _classificationService;

        public IRoleRepository AppRoleRepository { get; set; }

        public InvService(IInvRepository invRepository, IInvPositionRepository invPositionRepository, IUserRoleService userRoleService, IClassificationService classificationService)
        {
            _invRepository = invRepository;
            _invPositionRepository = invPositionRepository;
            _userRoleService = userRoleService;
            _classificationService = classificationService;
        }

        public object GetInvsOnStep(long stepId)
        {

            return _invRepository.GetInvsOnStep(stepId).Select(x => new
            {
                x.Id,
                x.FvNumber,
                x.ClientName,
                x.NetValue,
                x.GrossValue,
                x.DateOfIssue,
            }).ToList();

        }
        private List<Inv> GetInvsOnStepTyped(long stepId)
        {
            
            return _invRepository.GetInvsOnStep(stepId).ToList();

        }
        public object GetMyTasksList(Guid id)
        {
            List<Inv> myTasks = new List<Inv>();
            myTasks.AddRange(_invRepository.GetAll().Where(x => (x.Step == Dictionaries.StepEnum.Rejestracja && x.IntroducerId == id) || (x.Step == Dictionaries.StepEnum.Weryfikacja && x.MeritId == id)).ToList());

            if (_userRoleService.IsUserInRole(id, 3))
            {
                myTasks.AddRange(GetInvsOnStepTyped(5));
            }
            if (_userRoleService.IsUserInRole(id, 2))
            {
                myTasks.AddRange(GetInvsOnStepTyped(4));
            }
            if (_userRoleService.IsUserInRole(id, 1))
            {
                myTasks.AddRange(GetInvsOnStepTyped(3));
            }

            return myTasks.Select(x => new
            {
                x.Id,
                x.FvNumber,
                x.ClientName,
                x.NetValue,
                x.GrossValue,
                x.DateOfIssue,
            }).ToList();     
        }
        public object GetInvPositions(long id)
        {
            return _invPositionRepository.GetInvPositions(id).Select(x => new
            {
                x.Id,
                x.Descryption,
                x.NetValue,
                x.GrossValue,
                x.InvId,
            }).ToList();

        } 

        public InvDto StartInv(Guid userId)
        {
            Inv inv = new Inv()
            {
                IntroductionDate = DateTime.Now,
                Step = Dictionaries.StepEnum.Rejestracja,
                NetValue = 0,
                PaymentType = 1,
                FVType = 1,
                GrossValue = 0,
                Currency = 1,
                PaymentStatus = 1,
                VAT = 0,
                IntroducerId = userId,
            };
            _invRepository.Add(inv);
            _invRepository.Save();
            InvDto model = new InvDto()
            {
                Id = inv.Id,
                ClientName = inv.ClientName,
                NIP = inv.NIP,
                IntroductionDate = inv.IntroductionDate,
                FvNumber = inv.FvNumber,
                IntroducerId = inv.IntroducerId,
                MeritId = inv.MeritId,
                DateOfReceipt = inv.DateOfReceipt,
                DateOfIssue = inv.DateOfIssue,
                SaleDate = inv.SaleDate,
                DateOfPayment = inv.DateOfPayment,
                NetValue = inv.NetValue,
                PaymentType = inv.PaymentType,
                FVType = inv.FVType,
                GrossValue = inv.GrossValue,
                Currency = inv.Currency,
                PaymentStatus = inv.PaymentStatus,
                VAT = inv.VAT,
                Step = (int)inv.Step,
            };
            return model;
        }
        public InvDto GetInvDetails(long id)
        {

            Inv inv = _invRepository.GetInvDetails(id);
            InvDto model = new InvDto()
            {
                Id = inv.Id,
                ClientName = inv.ClientName,
                NIP = inv.NIP,
                IntroductionDate = inv.IntroductionDate,
                FvNumber = inv.FvNumber,
                IntroducerId = inv.IntroducerId,
                MeritId = inv.MeritId,
                DateOfReceipt = inv.DateOfReceipt,
                DateOfIssue = inv.DateOfIssue,
                SaleDate = inv.SaleDate,
                DateOfPayment = inv.DateOfPayment,
                NetValue = inv.NetValue,
                PaymentType = inv.PaymentType,
                FVType = inv.FVType,
                GrossValue = inv.GrossValue,
                Currency = inv.Currency,
                PaymentStatus = inv.PaymentStatus,
                VAT = inv.VAT,
                Step = (int)inv.Step,
                Positions = _invPositionRepository.GetInvPositions(id).Select(x => new InvPositionDto()
                {
                    Id = x.Id,
                    Descryption = x.Descryption,
                    Quantity = x.Quantity,
                    JM = x.JM,
                    NetValueUnit = x.NetValueUnit,
                    NetValue = x.NetValue,
                    VAT = x.VAT,
                    GrossValue = x.GrossValue,
                    InvId = x.InvId,
                    OverPaid = x.OverPaid,
                }).ToList(),
            };
            return model;
        }

        public void MoveInv(InvDto model)
        {
            Inv inv = _invRepository.GetInvDetails(model.Id);

            inv.ClientName = model.ClientName;
            inv.NIP = model.NIP;
            inv.IntroductionDate = model.IntroductionDate;
            inv.FvNumber = model.FvNumber;
            inv.IntroducerId = model.IntroducerId;
            inv.MeritId = model.MeritId;
            inv.DateOfReceipt = model.DateOfReceipt;
            inv.DateOfIssue = model.DateOfIssue;
            inv.SaleDate = model.SaleDate;
            inv.DateOfPayment = model.DateOfPayment;
            inv.NetValue = model.NetValue;
            inv.PaymentType = model.PaymentType;
            inv.FVType = model.FVType;
            inv.GrossValue = model.GrossValue;
            inv.Currency = model.Currency;
            inv.PaymentStatus = model.PaymentStatus;
            inv.VAT = model.VAT;
            if(inv.Step == Dictionaries.StepEnum.Weryfikacja)
            {
                if (_userRoleService.IsUserInRole(inv.IntroducerId.Value, 3))
                {
                    inv.Step = Dictionaries.StepEnum.AkceptacjaZarządu;
                }
                else if (_userRoleService.IsUserInRole(inv.IntroducerId.Value, 2))
                {
                    inv.Step = Dictionaries.StepEnum.AkceptacjaManagera;
                }
                else if (_userRoleService.IsUserInRole(inv.IntroducerId.Value, 1))
                {
                    inv.Step = Dictionaries.StepEnum.AkceptacjaKierownika;
                }
                else
                {
                    inv.Step = inv.Step + 1;
                }
                foreach (var item in inv.Positions)
                {
                    InvData invData = new InvData()
                    {
                        Id = item.Id,
                        Description = item.Descryption
                    };
                    int classType = _classificationService.Predict(invData);
                    item.Class = classType;
                    _invPositionRepository.Edit(item);
                    _invPositionRepository.Save();
                }
                foreach (var item in inv.Positions)
                {
                    List<InvPosition> documents = _invPositionRepository.GetAll(x => x.Class == item.Class).ToList();
                    decimal[] x = Array.Empty<decimal>();
                    foreach (var item2 in documents)
                    {
                        x = x.Append(item2.NetValueUnit).ToArray();
                    }
                    Array.Sort(x);
                    var q1 = NStats.Percentile(x, 0.25F);
                    var q3 = NStats.Percentile(x, 0.75F);
                    var iqr = q3 - q1;

                    var min = q1 - 1.5m * iqr;
                    var max = q3 + 1.5m * iqr;

                    if (item.NetValueUnit < min || item.NetValueUnit > max)
                    {
                        item.OverPaid = true;
                        _invPositionRepository.Edit(item);
                        _invPositionRepository.Save();
                    }
                }
            }
            else
            {
                inv.Step = inv.Step + 1;

            }
            _invRepository.Edit(inv);
            _invRepository.Save();

        }
        public void CancelInv(InvDto model)
        {
            Inv inv = _invRepository.GetInvDetails(model.Id);

            inv.ClientName = model.ClientName;
            inv.NIP = model.NIP;
            inv.IntroductionDate = model.IntroductionDate;
            inv.FvNumber = model.FvNumber;
            inv.IntroducerId = model.IntroducerId;
            inv.MeritId = model.MeritId;
            inv.DateOfReceipt = model.DateOfReceipt;
            inv.DateOfIssue = model.DateOfIssue;
            inv.SaleDate = model.SaleDate;
            inv.DateOfPayment = model.DateOfPayment;
            inv.NetValue = model.NetValue;
            inv.PaymentType = model.PaymentType;
            inv.FVType = model.FVType;
            inv.GrossValue = model.GrossValue;
            inv.Currency = model.Currency;
            inv.PaymentStatus = model.PaymentStatus;
            inv.VAT = model.VAT;

            inv.Step = Dictionaries.StepEnum.Zamknięte;
            _invRepository.Edit(inv);
            _invRepository.Save();

        }

        public void DeclineInv(InvDto model)
        {
            Inv inv = _invRepository.GetInvDetails(model.Id);

            inv.ClientName = model.ClientName;
            inv.NIP = model.NIP;
            inv.IntroductionDate = model.IntroductionDate;
            inv.FvNumber = model.FvNumber;
            inv.IntroducerId = model.IntroducerId;
            inv.MeritId = model.MeritId;
            inv.DateOfReceipt = model.DateOfReceipt;
            inv.DateOfIssue = model.DateOfIssue;
            inv.SaleDate = model.SaleDate;
            inv.DateOfPayment = model.DateOfPayment;
            inv.NetValue = model.NetValue;
            inv.PaymentType = model.PaymentType;
            inv.FVType = model.FVType;
            inv.GrossValue = model.GrossValue;
            inv.Currency = model.Currency;
            inv.PaymentStatus = model.PaymentStatus;
            inv.VAT = model.VAT;

            inv.Step = Dictionaries.StepEnum.Odrzucone;
            _invRepository.Edit(inv);
            _invRepository.Save();
        }


        public void AddInvPosition(InvPositionDto model)
        {

            InvPosition invPosition = new InvPosition()
            {
                Descryption = model.Descryption,
                Quantity = model.Quantity,
                JM = model.JM,
                NetValueUnit = model.NetValueUnit,
                NetValue = model.NetValue,
                VAT = model.VAT,
                GrossValue = model.GrossValue,
                InvId = model.InvId,
            };
            _invPositionRepository.Add(invPosition);
            _invPositionRepository.Save();
        }

        public InvPositionDto GetInvPositionDetails(long id)
        {

            InvPosition inv = _invPositionRepository.GetInvPositionDetails(id);
            InvPositionDto model = new InvPositionDto()
            {
                Id = inv.Id,
                Descryption = inv.Descryption,
                Quantity = inv.Quantity,
                JM = inv.JM,
                NetValueUnit = inv.NetValueUnit,
                NetValue = inv.NetValue,
                VAT = inv.VAT,
                GrossValue = inv.GrossValue,
                InvId = inv.InvId,
                OverPaid = inv.OverPaid,
            };
            return model;
        }
        public void EditInvPosition(InvPositionDto model)
        {

            InvPosition inv = _invPositionRepository.GetInvPositionDetails(model.Id.Value);
            inv.Descryption = model.Descryption;
            inv.Quantity = model.Quantity;
            inv.JM = model.JM;
            inv.NetValueUnit = model.NetValueUnit;
            inv.NetValue = model.NetValue;
            inv.VAT = model.VAT;
            inv.GrossValue = model.GrossValue;
            inv.InvId = model.InvId;

            _invPositionRepository.Edit(inv);
            _invPositionRepository.Save();
        }
        public void DeleteInvPosition(long id)
        {
            InvPosition inv = _invPositionRepository.GetInvPositionDetails(id);
            
            _invPositionRepository.Delete(inv);
            _invPositionRepository.Save();
        }

        /*internal decimal Percentile(decimal[] sortedData, decimal p)
        {
            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0m) return sortedData[sortedData.Length - 1];

            decimal position = (sortedData.Length + 1) * p / 100.0m;
            decimal leftNumber = 0.0m, rightNumber = 0.0m;

            decimal n = p / 100.0m * (sortedData.Length - 1) + 1.0m;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)Math.Floor(n) - 1];
                rightNumber = sortedData[(int)Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            //if (leftNumber == rightNumber)
            if (Equals(leftNumber, rightNumber))
                return leftNumber;
            decimal part = n - Math.Floor(n);
            return leftNumber + part * (rightNumber - leftNumber);
        }*/
    }
}
