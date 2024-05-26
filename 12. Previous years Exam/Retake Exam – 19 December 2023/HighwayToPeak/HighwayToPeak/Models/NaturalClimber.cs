namespace HighwayToPeak.Models
{
    public class NaturalClimber : Climber
    {
        private const int initialStamina = 6;
        private const int staminaRecovery = 2;

        public NaturalClimber(string name) : base(name, initialStamina)
        {
        }

        public override void Rest(int daysCount)
        {
            Stamina += daysCount * staminaRecovery;
        }
    }

}

