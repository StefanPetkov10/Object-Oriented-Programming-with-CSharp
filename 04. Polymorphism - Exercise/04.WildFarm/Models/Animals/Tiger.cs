using WildFarm.Models.Food;


namespace WildFarm.Models.Animals;

public class Tiger : Feline
{
    private const double TigerWeightIncreasePerFood = 1;

    public Tiger(string name, double weight, string livingRegion, string breed)
        : base(name, weight, livingRegion, breed)
    { }

    protected override double WeightIncreasePerFood
        => TigerWeightIncreasePerFood;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
    => new HashSet<Type>() { typeof(Meat) };

    public override string ProduceSound()
        => "ROAR!!!";
}