namespace WebApplicationLab.Models
{
    public class Calculator
    {
        public int firstNumber
        {
            get; set;
        }
        public int secondNumber
        {
            get; set;
        }
        public int add()
        { 
            return firstNumber+secondNumber;
        }
    }
}
