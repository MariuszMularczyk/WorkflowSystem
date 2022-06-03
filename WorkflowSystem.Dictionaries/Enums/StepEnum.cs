using System;
using System.Collections.Generic;
using System.Text;

namespace WorkflowSystem.Dictionaries
{
    public enum StepEnum : int
    {
        Rejestracja = 1,
        Weryfikacja = 2,
        AkceptacjaKierownika = 3,
        AkceptacjaManagera = 4,
        AkceptacjaZarządu = 5,
        Zaakceptowany = 6,
        Odebrany = 7,
        Odrzucone = 10,
        Zamknięte = 99,
    }
}
