using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPC
{
    class Program
    {
        public enum SCPType { Stone = 1, Scissors = 2, Paper = 3 }


        private static string GetTypeName(SCPType scpType)
        {
            string res = String.Empty;

            switch (scpType)
            {
                case SCPType.Stone:
                    {
                        res = "Stone";
                        break;
                    }

                case SCPType.Scissors:
                    {
                        res = "Scissors";
                        break;
                    }

                case SCPType.Paper:
                    {
                        res = "Paper";
                        break;
                    }
            }

            return res;
        }

        private static SCPType GenerateCPUStep()
        {
            Random r = new Random();
            return (SCPType)(r.Next(1, 4));
        }

        private static bool Victory(SCPType user, SCPType cpu)
        {
            bool res = false;

            res = (
                    (user == SCPType.Scissors && cpu == SCPType.Paper) ||
                    (user == SCPType.Stone && cpu == SCPType.Scissors) ||
                    (user == SCPType.Paper && cpu == SCPType.Stone)
                  );

            return res;
        }

        static void Main(string[] args)
        {
            SCPType cpu = GenerateCPUStep();
            Console.WriteLine(String.Format("Choose your value: {0} Stone: 1 {0} Scissors: 2  {0} Paper: 3 {0} And press Enter", Environment.NewLine));

            int intUser = 0;
            while (intUser < 1)
            {
                string strUserValue = Console.ReadLine();
                int.TryParse(strUserValue, out intUser);
                if (intUser < 0 || !(intUser > 0 && intUser < 4))
                    Console.WriteLine("You must enter a numeric value from 1 to 3 ");
            }

            SCPType user = (SCPType)intUser;
            if (cpu == user)
                Console.WriteLine(String.Format("Draw. You and your computer have chosen  {0}", GetTypeName(cpu)));
            else if (Victory(user, cpu))
                Console.WriteLine(String.Format("You won. You selected {0} and computer {1} ", GetTypeName(user), GetTypeName(cpu)));
            else
                Console.WriteLine(String.Format("You lose. You selected {0} and computer {1} ", GetTypeName(user), GetTypeName(cpu)));

            Console.WriteLine("Press enter to exit ");
            Console.ReadLine();
        }
    }
}