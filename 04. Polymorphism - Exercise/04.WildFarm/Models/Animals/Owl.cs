using WildFarm.Models.Food;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        private const double OwlWeightIncreasePerFood = 0.25;
        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        protected override double WeightIncreasePerFood => OwlWeightIncreasePerFood;

        protected override IReadOnlyCollection<Type> PreferredFoodTypes
            => new HashSet<Type> { typeof(Meat) };

        public override string ProduceSound()
            => "Hoot Hoot";
    }
}
