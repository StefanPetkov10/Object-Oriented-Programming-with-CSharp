using WildFarm.Models.Food;


namespace WildFarm.Models.Animals;

public class Cat : Feline
{
    private const double CatWeightIncreasePerFood = 0.3;

    public Cat(string name, double weight, string livingRegion, string breed)
        : base(name, weight, livingRegion, breed) { }

    protected override double WeightIncreasePerFood
        => CatWeightIncreasePerFood;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type>() { typeof(Vegetable), typeof(Meat) };

    public override string ProduceSound()
        => "Meow";
}