class Counter
{
    const int triggerNum = 77;
    public delegate void Messenger(int trigger);
    public event Messenger Notify;

    public Counter()
    {
        Notify += Handler1.EventHandler;
        Notify += Handler2.EventHandler;
    }
    public void Count()
    {


        for (int i = 1; i <= 100; ++i)
        {
            Console.WriteLine(i);
            if (i == triggerNum) Notify.Invoke(i);
        }
    }
}