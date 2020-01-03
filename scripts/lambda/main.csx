
Func<DateTime, bool> canDrive = dob => {
    return dob.AddYears(18) <= DateTime.Today;
};

DateTime dob = new DateTime(2000,12,25);

bool result = canDrive(dob);

Console.WriteLine(result);


Action<DateTime> printDate = date => Console.WriteLine(date);

DateTime date = DateTime.Today;
printDate(date);


Func<string, string, string> conc = (nome1, nome2) => nome1 + nome2;

string nome1 = "Ciao";
string nome2 = "Asgara";


string concatenazione = conc(nome1, nome2);

Console.WriteLine(concatenazione);