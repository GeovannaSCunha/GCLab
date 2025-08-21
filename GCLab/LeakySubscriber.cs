namespace GCLab;

using System;

class LeakySubscriber
{
    public LeakySubscriber(Publisher publisher)
    {
        // Assina normalmente; o Publisher usa weak event,
        // então não mantém este objeto vivo.
        publisher.OnSomething += OnRaised;
    }

    private void OnRaised() { /* uso didático */ }
}
