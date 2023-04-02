public static class ORGAN_STATES
/*
Чтобы в программе не было магических чисел 
(в данном случае, магических булевых переменных), и другой программист,
копаясь в моем коде, не вспоминал меня нехорошими словами,
я сделал статический класс, в котором прописал все константы.
*/
{
    public const bool HEALTHY = true;
    public const bool UNHEALTHY = false;

    public const bool RIGHT = true;
    public const bool LEFT = false;
}

public abstract class Organ
{
    public Organ()
    { }
    public string name = "";
    public bool state = ORGAN_STATES.HEALTHY;

}

public class Heart : Organ
{
    public Heart() { name = "Сердце"; }
}
public class Liver : Organ
{
    public Liver() { name = "Печень"; }
}

public class Kidney : Organ
{
    public Kidney() { name = "Почка"; }
}

public class Mentality : Organ
{
    public Mentality() { name = "Психика"; }
}

public class Gaster : Organ
{
    public Gaster() { name = "Желудок"; }
}

public class Intestine : Organ
{
    public Intestine() { name = "Кишечник"; }
}