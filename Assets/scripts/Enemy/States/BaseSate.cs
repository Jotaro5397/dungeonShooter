public abstract class BaseState
{
    public Enemy enemy;

    public StateMachine stateMachine;
    //instacne of statemachine classs

    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();
}
