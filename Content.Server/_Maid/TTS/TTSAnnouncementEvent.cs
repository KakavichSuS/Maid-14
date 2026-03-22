namespace Content.Server._Maid.TTS;

/// <summary>
/// When an announcement is placed on the station, this plays TTS
/// to the nearest players using the objects specified in <see cref="TTSSystem"/>
/// </summary>
/// <param name="message"></param>
/// <param name="voiceId"></param>
/// <param name="source"></param>
/// <param name="global"></param>
public sealed class TTSAnnouncementEvent(string message, string voiceId, EntityUid source, bool global)
    : EntityEventArgs
{
    public readonly string Message = message;
    public readonly bool Global = global;
    public readonly string VoiceId = voiceId;
    public readonly EntityUid Source = source;
}
