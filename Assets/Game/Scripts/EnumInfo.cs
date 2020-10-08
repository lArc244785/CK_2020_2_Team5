
[System.Serializable]
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
        None, Title, Ingame, Pause, GameOver, StageMove, Loading, ItemGet, GameClear
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

    public enum Item 
    {
        Power, Range, Reload, Speed, HP
    }

    public enum PadeinOutOption
    {
        Nomal, Icon,horizon,StageMove
    }

    

}
