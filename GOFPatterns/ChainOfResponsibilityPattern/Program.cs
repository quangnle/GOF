using System;

namespace ChainOfResponsibilityPattern
{
    public class Client
    {
        public static void Main(string[] args)
        {
            UserProfile profile1 = new UserProfile { Name = "Tony", Age = 28, Email = "tony@company.com" };
            UserProfile profile2 = new UserProfile { Name = "Tim", Age = 128, Email = "tim@company.com" };

            Validator nameValidator = new NameValidator();
            Validator ageValidator = new AgeValidator();
            Validator emailValidator = new EmailValidator();

            nameValidator.NextValidator(ageValidator);
            ageValidator.NextValidator(emailValidator);

            Console.WriteLine(nameValidator.Validate(profile1));
            Console.WriteLine(nameValidator.Validate(profile2));
        }
    }

    public class UserProfile
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    public abstract class Validator // Handler
    {
        protected Validator _successor;
        public abstract bool Validate(UserProfile profile); // HandleRequest

        public void NextValidator(Validator successor)
        {
            _successor = successor;
        }
    }

    public class NameValidator : Validator // Concrete Handler
    {
        public override bool Validate(UserProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Name)) return false;

            if (_successor != null) return _successor.Validate(profile);

            return true;
        }
    }

    public class AgeValidator : Validator // Concrete Handler
    {
        public override bool Validate(UserProfile profile)
        {
            if (profile.Age < 18 || profile.Age > 99) return false;

            if (_successor != null) return _successor.Validate(profile);

            return true;
        }
    }

    public class EmailValidator : Validator // Concrete Handler
    {
        public override bool Validate(UserProfile profile)
        {
            if (string.IsNullOrEmpty(profile.Email)) return false;

            if (_successor != null) return _successor.Validate(profile);

            return true;
        }
    }
}
