
public class PlayerStateFactory 
{
    PlayerStateManager _context;
    public PlayerStateFactory( PlayerStateManager currentcontext)
    {
        _context = currentcontext;
    }
    public PlayerBaseState Idle()
    {
        return new PlayerIdle(_context,this);
    }

    public PlayerBaseState Dead()
    {
        return new PlayerDead(_context,this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalk(_context,this);
    }

    public PlayerBaseState Jump() 
    {
        return new PlayerJump(_context,this);
    }

    public PlayerBaseState IsGrounded() 
    {   
        return new PlayerIsGrounded(_context,this);
    }
    public PlayerBaseState Fall()
    {
        return new PlayerFall(_context,this); 
    }
    public PlayerBaseState DoubleJump()
    {
        return new PlayerDoubleJump(_context,this); 
    }
    public PlayerBaseState InSpace()
    {
        return new PlayerInSpace(_context,this); 
    }
    public PlayerBaseState WallSlide()
    {
        return new PlayerWallSlide(_context,this);
    }
}
