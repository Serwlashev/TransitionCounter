namespace TransitionCounter.Services.Interfaces
{
    interface ISessionService
    {
        void CountTransition(string key);
        int GetTransitionNumber(string key);
    }
}
