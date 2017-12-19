[System.Serializable]
public class Attribute
{
    public enum Type
    {
        Playfullness, Strength, Lazyness, Toughness, Curiosity, Sociality
    }

    public Type type;
    public int value = 0;

    public Attribute()
    {
    }

    public Attribute(Type type, int value)
    {
        this.type = type;
        this.value = value;
    }
}