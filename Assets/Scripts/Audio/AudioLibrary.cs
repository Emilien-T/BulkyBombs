using AudioSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLibrary : Controller<AudioLibrary>
{
    [Header("AudioMixerGroups")]
    public AudioMixerGroup Master;
    public AudioMixerGroup SFX;
    public AudioMixerGroup Music;

    [Header("Music")]
    public AudioClip BombFactoryAmbienceClip;
    public AudioClip ZenAmbienceClip;

    [Header("SFX")]
    public AudioClip BombComingInClip;
    public AudioClip BombGoingOutClip;
    public AudioClip BoxClosingClip;
    public AudioClip BoxScrapingClip;
    public AudioClip CalmingDownClip;
    public AudioClip ConveyorClip;
    public AudioClip GettingMadClip;
    public AudioClip HammeringClip;
    public AudioClip LowTimeClip;
    public AudioClip NuclearExplosionClip;
    public AudioClip PolishClip;
    public AudioClip SadTromboneClip;
    public AudioClip SweepToWorkClip;
    public AudioClip SweepToZenClip;
    public AudioClip TimerBeepClip;
    public AudioClip WhooshClip;
    public AudioClip WrenchClip;

    // ===================== SOUND DATA =====================

    SoundData BombFactoryAmbienceData;
    SoundData ZenAmbienceData;

    [Header("SFX")]
    SoundData BombComingInData;
    SoundData BombGoingOutData;
    SoundData BoxClosingData;
    SoundData BoxScrapingData;
    SoundData CalmingDownData;
    SoundData ConveyorData;
    SoundData GettingMadData;
    SoundData HammeringData;
    SoundData LowTimeData;
    SoundData NuclearExplosionData;
    SoundData PolishData;
    SoundData SadTromboneData;
    SoundData SweepToWorkData;
    SoundData SweepToZenData;
    SoundData TimerBeepData;
    SoundData WhooshData;
    SoundData WrenchData;

    // ===================== Music =====================
    protected override void MyAwake()
    {
        // Music
        BombFactoryAmbienceData = new(BombFactoryAmbienceClip, true, false, Music, false, false);
        ZenAmbienceData = new(ZenAmbienceClip, true, false, Music, false, false);

        // SFX
        BombComingInData = new(BombComingInClip, false, false, SFX, false, false);
        BombGoingOutData = new(BombGoingOutClip, false, false, SFX, false, false);
        BoxClosingData = new(BoxClosingClip, false, false, SFX, false, false);
        BoxScrapingData = new(BoxScrapingClip, false, false, SFX, false, false);
        CalmingDownData = new(CalmingDownClip, false, false, SFX, false, false);
        ConveyorData = new(ConveyorClip, false, false, SFX, false, false);
        GettingMadData = new(GettingMadClip, false, false, SFX, false, false);
        HammeringData = new(HammeringClip, false, false, SFX, false, false);
        LowTimeData = new(LowTimeClip, false, false, SFX, false, false);
        NuclearExplosionData = new(NuclearExplosionClip, false, false, SFX, false, false);
        PolishData = new(PolishClip, false, false, SFX, false, false);
        SadTromboneData = new(PolishClip, false, false, SFX, false, false);
        SweepToWorkData = new(SweepToWorkClip, false, false, SFX, false, false);
        SweepToZenData = new(SweepToZenClip, false, false, SFX, false, false);
        TimerBeepData = new(TimerBeepClip, false, false, SFX, false, false);
        WhooshData = new(WhooshClip, false, false, SFX, false, false);
        WrenchData = new(WrenchClip, false, false, SFX, false, false);
    }

    // ===================== Music =====================

    public void StartBombFactoryAmbience()
    {
        if (SoundManager.Instance.mainMusic != null) SoundManager.Instance.mainMusic.Stop();
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(BombFactoryAmbienceData).Play();
    }

    public void StartZenAmbience()
    {
        if (SoundManager.Instance.mainMusic != null) SoundManager.Instance.mainMusic.Stop();
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(ZenAmbienceData).Play();
    }

    public void StopMainMusic()
    {
        if (SoundManager.Instance.mainMusic != null)
        {
            SoundManager.Instance.mainMusic.Stop();
            SoundManager.Instance.mainMusic = null;
        }
    }

    // ===================== SFX =====================

    public SoundBuilder BombComingIn() => Play(BombComingInData);
    public SoundBuilder BombGoingOut() => Play(BombGoingOutData);
    public SoundBuilder BoxClosing() => Play(BoxClosingData);
    public SoundBuilder BoxScraping() => Play(BoxScrapingData);
    public SoundBuilder CalmingDown() => Play(CalmingDownData);
    public SoundBuilder Conveyor() => Play(ConveyorData);
    public SoundBuilder GettingMad() => Play(GettingMadData);
    public SoundBuilder Hammering() => Play(HammeringData);
    public SoundBuilder LowTime() => Play(LowTimeData);
    public SoundBuilder NuclearExplosion() => Play(NuclearExplosionData);
    public SoundBuilder Polish() => Play(PolishData);
    public SoundBuilder SadTrombone() => Play(SadTromboneData);
    public SoundBuilder SweepToWork() => Play(SweepToWorkData);
    public SoundBuilder SweepToZen() => Play(SweepToZenData);
    public SoundBuilder TimerBeep() => Play(TimerBeepData);
    public SoundBuilder Whoosh() => Play(WhooshData);
    public SoundBuilder Wrench() => Play(WrenchData);
    // ===================== HELPER =====================
    SoundBuilder Play(SoundData data)
    {
        if (data.clip == null) return null;
        return SoundManager.Instance.CreateSound().WithSoundData(data).Play();
    }

    SoundBuilder Play(SoundData data, bool isP1)
    {
        if (data.clip == null) return null;
        return SoundManager.Instance.CreateSound().WithSoundData(data).WithStereoPan(isP1 ? -0.5f : 0.5f).Play();
    }
}