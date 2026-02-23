using Robust.Shared.Player;

namespace Content.Server._Maid.GhostRecruitment;

[Serializable]
public sealed class GhostRecruitmentSuccessEvent : EntityEventArgs
{
    public string RecruitmentName;
    public ICommonSession PlayerSession;

    public GhostRecruitmentSuccessEvent(string recruitmentName, ICommonSession playerSession)
    {
        RecruitmentName = recruitmentName;
        PlayerSession = playerSession;
    }
}
