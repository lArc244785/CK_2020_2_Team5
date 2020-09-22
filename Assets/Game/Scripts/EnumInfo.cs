

public class EnumInfo
{

    public enum CanonState
    {
        NoEmpty, Reroad, BulletOn
    }

    public enum CamType
    {
        None, Target, Fixing
    }

    public enum GameState
    {
        None, Title, Ingame, Pause, GameOver, StageMove
    }

    public enum DoorSpawn
    {
        Left , Right , Up, Down
    }


    public enum MonsterState
    {
        Move = 1,
        Attack,
        CoolDown //임시 제작
    }

    public enum ItemGet 
    {
        Power, Range, Reload, Speed
    }



}
