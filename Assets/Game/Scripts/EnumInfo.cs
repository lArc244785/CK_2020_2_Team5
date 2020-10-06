

public class EnumInfo
{

    public enum MagazineUI
    {
        NoEmpty, Reroad
    }

    public enum CamType
    {
        None, Target, Fixing
    }

    public enum GameState
    {
        None, StateChoice, Ingame
    }

    public enum DoorSpawn
    {
        Left , Right , Up, Down
    }

    public enum MonsterState
    {
        Move = 1, //배회 상태
        Attack,
        CoolDown, //임시 제작
        Wait,
        Trace,
        Die,
        RandomMove,
        Turn
    }
}
