namespace _Framework.Application;

public  interface IUnitOfWork
{
    void BeginTrans();
    void Commit();
    void RollBack();
}
