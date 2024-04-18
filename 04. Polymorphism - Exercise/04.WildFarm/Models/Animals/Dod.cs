using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Dog : Mammal
{
    private const double DogWeightIncreasePerFood = 0.4;

    public Dog(string name, double weight, string livingRegion)
        : base(name, weight, livingRegion)
    { }

    protected override double WeightIncreasePerFood
        => DogWeightIncreasePerFood;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type>() { typeof(Meat) };

    public override string ProduceSound()
        => "Woof!";

    public override string ToString()
        => base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
}