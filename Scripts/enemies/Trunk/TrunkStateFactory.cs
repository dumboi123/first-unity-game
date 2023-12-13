public class TrunkStateFactory 
{
    TrunkStateManager _context;
    public TrunkStateFactory( TrunkStateManager currentcontext)
    {
        _context = currentcontext;
    }
    public TrunkBaseState Idle()
    {
        return new TrunkIdle(_context,this);
    }

    public TrunkBaseState Patrol()
    {
        return new TrunkPatrol(_context,this);
    }

    public TrunkBaseState Shoot()
    {
        return new TrunkShoot(_context,this);
    }
}