using AudioSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLibrary : Controller<AudioLibrary>
{
    [Header("Design")]
    public bool movesPlayedStereod = true;
    public bool moveRegisteredStereod = true;
    public bool characterSelectStereod = true;
    public bool meterFillStereod = true;
    [Header("AudioMixerGroups")]
    public AudioMixerGroup Master;
    public AudioMixerGroup SFX;
    public AudioMixerGroup Music;
    public AudioMixerGroup CarMixer;

    [Header("Music")]
    public AudioClip mainMenuMusicClip;
    public AudioClip charSelectMusicClip;
    public AudioClip gameplayMusic;
    public AudioClip metronomeMusic;

    [Header("UI / Menu")]
    public AudioClip connectClip;
    public AudioClip buttonHover01Clip;
    public AudioClip buttonHover02Clip;
    public AudioClip buttonHover03Clip;
    public AudioClip buttonSelectClip;
    public AudioClip venueSwooshClip;
    public AudioClip optionsNumberChangeClip;
    public AudioClip optionsToggleClip;
    public AudioClip dialogueSkipClip;
    public AudioClip oneTwoThreeClip;
    public AudioClip finishThemClip;
    public List<AudioClip> popSelectClips = new();
    public List<AudioClip> hiphopSelectClips = new();
    public List<AudioClip> smallCheerClips = new();
    public AudioClip metronomeClip;
    public AudioClip startButtonClip;
    public AudioClip optionsButtonClip;
    public AudioClip creditsButtonClip;
    public AudioClip quitButtonsClip;

    [Header("Character Select")]
    public AudioClip characterSwitchClip;
    public List<AudioClip> characterSelectedClips = new();
    public List<AudioClip> charSelectTransitionClips = new();

    [Header("Gameplay - Core")]
    public AudioClip moveRegisteredClip;
    public List<AudioClip> moveRegisteredOnBeatClip = new();
    public AudioClip timerLowClip;
    public AudioClip timerUpClip;

    [Header("Gameplay - Meter")]
    public AudioClip meterFillingClip;
    public AudioClip meterAlmostFullClip;
    public AudioClip meterOverflowClip;

    public AudioClip meterPulseLowClip;
    public AudioClip meterPulseMedClip;
    public AudioClip meterPulseHighClip;

    [Header("Gameplay - Timing")]
    public AudioClip countdownFinalBeatsClip;

    [Header("Gameplay - Misc")]
    public List<AudioClip> simpleStabsClips = new();
    public AudioClip lightAttackWinClip;
    public AudioClip heavyAttackWinClip;
    public AudioClip chargeAttackWinClip;
    public AudioClip parryAttackWinClip;

    public AudioClip lightAttackLoseClip;
    public AudioClip heavyAttackLoseClip;
    public AudioClip chargeAttackLoseClip;
    public AudioClip parryAttackLoseClip;

    public AudioClip parryAttackNeutralClip;
    public AudioClip domainExpansionClip;
    public AudioClip loudCheerClip;
    public List<AudioClip> tutorialDialogueClips = new();

    public AudioClip tutorialPromptClip;
    public AudioClip tutorialYesNoClip;

    public AudioClip laser01Clip;
    public AudioClip laser02Clip;
    public AudioClip carClip;
    public AudioClip badInputClip;

    // ===================== SOUND DATA =====================

    // Music
    SoundData gameplayMusicData;
    SoundData metronomeMusicData;
    SoundData mainMenuMusicData;
    SoundData charSelectMusicData;

    // UI
    SoundData connectData;
    SoundData buttonHover01Data;
    SoundData buttonHover02Data;
    SoundData buttonHover03Data;
    SoundData buttonSelectData;
    SoundData venueSwooshData;
    SoundData optionsNumberChangeData;
    SoundData optionsToggleData;
    SoundData dialogueSkipData;
    SoundData oneTwoThreeData;
    SoundData finishThemData;
    List<SoundData> popSelectData = new();
    List<SoundData> hiphopSelectData = new();
    List<SoundData> smallCheerData = new();
    SoundData metronomeData;
    SoundData startButtonData;
    SoundData optionsButtonData;
    SoundData creditsButtonData;
    SoundData quitButtonsData;

    // Character Select
    SoundData characterSwitchData;
    List<SoundData> characterSelectedData = new();
    List<SoundData> charSelectTransitionData = new();

    // Gameplay Core
    SoundData moveRegisteredData;
    List<SoundData> moveRegisteredOnBeatData = new();
    SoundData timerLowData;
    SoundData timerUpData;

    // Meter
    SoundData meterFillingData;
    SoundData meterAlmostFullData;
    SoundData meterOverflowData;
    SoundData meterPulseLowData;
    SoundData meterPulseMedData;
    SoundData meterPulseHighData;

    // Timing
    SoundData countdownFinalBeatsData;

    // Attacks
    List<SoundData> simpleStabsData = new();

    SoundData lightAttackWinData;
    SoundData heavyAttackWinData;
    SoundData chargeAttackWinData;
    SoundData parryAttackWinData;

    SoundData lightAttackLoseData;
    SoundData heavyAttackLoseData;
    SoundData chargeAttackLoseData;
    SoundData parryAttackLoseData;

    SoundData parryAttackNeutralData;
    SoundData domainExpansionData;
    SoundData loudCheerData;
    List<SoundData> tutorialDialogueData = new();
    SoundData tutorialPromptData;
    SoundData tutorialYesNoData;

    SoundData laser01Data;
    SoundData laser02Data;
    SoundData carData;
    SoundData badInputData;

    private float prevSimpleStabTimestamp = 0;
    private float simpleStabDebounce = 0.2f;

    // Randomized
    private int prevSimpleStabIndex = -1;
    private int prevOnBeatBonusIndex = -1;
    private int prevPopSelectIndex = -1;
    private int prevHipHopSelectIndex = -1;
    private int prevCharSelectIndex = -1;
    private int prevCharSelectTransitionIndex = -1;
    private int prevTutorialDialogueIndex = -1;
    private int prevSmallCheerIndex = -1;

    protected override void MyAwake()
    {
        // Music
        gameplayMusicData = new(gameplayMusic, true, false, Music, false, false);
        metronomeMusicData = new(metronomeMusic, true, false, Music, false, false);
        mainMenuMusicData = new(mainMenuMusicClip, true, false, Music, false, false, 0.7f);
        charSelectMusicData = new(charSelectMusicClip, true, false, Music, false, false);
        // UI
        connectData = new(connectClip, false, false, SFX, false, false);
        buttonHover01Data = new(buttonHover01Clip, false, false, SFX, false, false);
        buttonHover02Data = new(buttonHover02Clip, false, false, SFX, false, false);
        buttonHover03Data = new(buttonHover03Clip, false, false, SFX, false, false);
        buttonSelectData = new(buttonSelectClip, false, false, SFX, true, false);
        venueSwooshData = new(venueSwooshClip, false, false, SFX, false, true);
        optionsNumberChangeData = new(optionsNumberChangeClip, false, false, SFX, false, true);
        optionsToggleData = new(optionsToggleClip, false, false, SFX, false, true);
        dialogueSkipData = new(dialogueSkipClip, false, false, SFX, false, false);
        oneTwoThreeData = new(oneTwoThreeClip, false, false, SFX, false, false);
        finishThemData = new(finishThemClip, false, false, SFX, false, false);
        metronomeData = new(metronomeClip, false, false, SFX, false, false);
        startButtonData = new(startButtonClip, false, false, SFX, false, false);
        optionsButtonData = new(optionsButtonClip, false, false, SFX, false, false);
        creditsButtonData = new(creditsButtonClip, false, false, SFX, false, false);
        quitButtonsData = new(quitButtonsClip, false, false, SFX, false, false);

        for (int i = 0; i < popSelectClips.Count; i++)
        {
            popSelectData.Add(new SoundData(popSelectClips[i], false, false, SFX, false, true, 0.5f));
        }
        for (int i = 0; i < hiphopSelectClips.Count; i++)
        {
            hiphopSelectData.Add(new SoundData(hiphopSelectClips[i], false, false, SFX, false, true, 0.5f));
        }
        for (int i = 0; i < smallCheerClips.Count; i++)
        {
            smallCheerData.Add(new SoundData(smallCheerClips[i], false, false, SFX, false, false));
        }

        // Character Select
        characterSwitchData = new(characterSwitchClip, false, false, SFX, false, true);
        for (int i = 0; i < characterSelectedClips.Count; i++)
        {
            characterSelectedData.Add(new SoundData(characterSelectedClips[i], false, false, SFX, false, true));
        }
        for (int i = 0; i < charSelectTransitionClips.Count; i++)
        {
            charSelectTransitionData.Add(new SoundData(charSelectTransitionClips[i], false, false, SFX, false, false));
        }

        // Gameplay Core
        moveRegisteredData = new(moveRegisteredClip, false, false, SFX, false, true);
        for (int i = 0; i < moveRegisteredOnBeatClip.Count; i++)
        {
            moveRegisteredOnBeatData.Add(new SoundData(moveRegisteredOnBeatClip[i], false, false, SFX, false, false));
        }
        timerLowData = new(timerLowClip, false, false, SFX, false, false);
        timerUpData = new(timerUpClip, false, false, SFX, false, false);

        // Meter
        meterFillingData = new(meterFillingClip, false, false, SFX, true, false);
        meterAlmostFullData = new(meterAlmostFullClip, false, false, SFX, false, false);
        meterOverflowData = new(meterOverflowClip, false, false, SFX, false, false);
        meterPulseLowData = new(meterPulseLowClip, true, false, SFX, false, false);
        meterPulseMedData = new(meterPulseMedClip, true, false, SFX, false, false);
        meterPulseHighData = new(meterPulseHighClip, true, false, SFX, false, false);

        // Timing
        countdownFinalBeatsData = new(countdownFinalBeatsClip, false, false, SFX, false, false);

        // Attacks
        for (int i = 0; i < simpleStabsClips.Count; i++)
        {
            simpleStabsData.Add(new SoundData(simpleStabsClips[i], false, false, SFX, false, false, 0.7f));
        }

        lightAttackWinData = new(lightAttackWinClip, false, false, SFX, false, true);
        heavyAttackWinData = new(heavyAttackWinClip, false, false, SFX, false, false);
        chargeAttackWinData = new(chargeAttackWinClip, false, false, SFX, false, false);
        parryAttackWinData = new(parryAttackWinClip, false, false, SFX, false, true);

        lightAttackLoseData = new(lightAttackLoseClip, false, false, SFX, false, true);
        heavyAttackLoseData = new(heavyAttackLoseClip, false, false, SFX, false, false);
        chargeAttackLoseData = new(chargeAttackLoseClip, false, false, SFX, false, false);
        parryAttackLoseData = new(parryAttackLoseClip, false, false, SFX, false, true);

        parryAttackNeutralData = new(parryAttackNeutralClip, false, false, SFX, false, true);
        domainExpansionData = new(domainExpansionClip, false, false, SFX, false, false);
        loudCheerData = new(loudCheerClip, true, false, SFX, false, false);

        for (int i = 0; i < tutorialDialogueClips.Count; i++)
        {
            tutorialDialogueData.Add(new SoundData(tutorialDialogueClips[i], false, false, SFX, false, true, 0.5f));
        }
        tutorialPromptData = new(tutorialPromptClip, false, false, SFX, false, false);
        tutorialYesNoData = new(tutorialYesNoClip, false, false, SFX, false, false);

        laser01Data = new(laser01Clip, false, false, SFX, false, false);
        laser02Data = new(laser02Clip, false, false, SFX, false, false);
        carData = new(carClip, false, false, CarMixer, false, true);
        badInputData = new(badInputClip, false, false, SFX, false, false);
    }

    // ===================== Music =====================
    public void StartGameplayMusic()
    {
        if(SoundManager.Instance.mainMusic != null) SoundManager.Instance.mainMusic.Stop();
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(gameplayMusicData).Play();
    }
    public SoundBuilder SampleGameplayMusic() => Play(gameplayMusicData);
    public void StartMainMenuMusic()
    {
        if (SoundManager.Instance.mainMusic != null) SoundManager.Instance.mainMusic.Stop();
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(mainMenuMusicData).Play();
    }
    public void StartCharSelectMusic()
    {
        if (SoundManager.Instance.mainMusic != null) SoundManager.Instance.mainMusic.Stop();
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(charSelectMusicData).Play();
    }
    public void StopMainMusic() 
    {
        if (SoundManager.Instance.mainMusic != null) 
        {
            SoundManager.Instance.mainMusic.Stop();
            SoundManager.Instance.mainMusic = null;
        }
    }
    public void StartMetronome()
    {
        SoundManager.Instance.mainMusic = SoundManager.Instance.CreateSound().WithSoundData(gameplayMusicData).Play();
    }

    // ===================== UI =====================
    public SoundBuilder Connect() => Play(connectData);
    public SoundBuilder ButtonHover01() => Play(buttonHover01Data);
    public SoundBuilder ButtonHover02() => Play(buttonHover02Data);
    public SoundBuilder ButtonHover03() => Play(buttonHover03Data);
    public SoundBuilder ButtonSelect() => Play(buttonSelectData);
    public SoundBuilder VenueSwoosh() => Play(venueSwooshData);
    public SoundBuilder OptionsNumberChange() => Play(optionsNumberChangeData);
    public SoundBuilder OptionsToggle() => Play(optionsToggleData);
    public SoundBuilder DialogueSkip() => Play(dialogueSkipData);
    public SoundBuilder OneTwoThree() => Play(oneTwoThreeData);
    public SoundBuilder FinishThem() => Play(finishThemData);
    public SoundBuilder Metronome() => Play(metronomeData);
    public SoundBuilder OptionsButton() => Play(optionsButtonData);
    public SoundBuilder StartButton() => Play(startButtonData);
    public SoundBuilder CreditsButton() => Play(creditsButtonData);
    public SoundBuilder QuitButton() => Play(quitButtonsData);

    // ===================== CHARACTER SELECT =====================
    public SoundBuilder CharacterSwitch(bool isP1) 
    {
        if (characterSelectStereod)
            return Play(characterSwitchData, isP1);
        else
            return Play(characterSwitchData);
    }
    public SoundBuilder CharacterSelected(bool isP1)
    {
        if (characterSelectedData.Count == 0) return null;
        int index = Random.Range(0, characterSelectedData.Count - 1);
        index = index == prevCharSelectIndex ? characterSelectedData.Count - 1 : index;
        if (index == -1) index = 0;
        prevCharSelectIndex = index;
        return Play(characterSelectedData[index]);
    }
    public SoundBuilder CharacterSelectTransition()
    {
        if (charSelectTransitionData.Count == 0) return null;
        int index = Random.Range(0, charSelectTransitionData.Count - 1);
        index = index == prevCharSelectTransitionIndex ? charSelectTransitionData.Count - 1 : index;
        if (index == -1) index = 0;
        prevCharSelectTransitionIndex = index; 
        return Play(charSelectTransitionData[index]);
    }
    public SoundBuilder PopSelect(bool isP1)
    {
        if (popSelectData.Count == 0) return null;
        int index = Random.Range(0, popSelectData.Count - 1);
        index = index == prevPopSelectIndex ? popSelectData.Count - 1 : index;
        if (index == -1) index = 0;
        prevOnBeatBonusIndex = index;

        if (characterSelectStereod)
            return Play(popSelectData[index], isP1);
        else
            return Play(popSelectData[index]);
    }
    public SoundBuilder HipHopSelect(bool isP1)
    {
        if (hiphopSelectData.Count == 0) return null;
        int index = Random.Range(0, hiphopSelectData.Count - 1);
        index = index == prevHipHopSelectIndex ? hiphopSelectData.Count - 1 : index;
        if (index == -1) index = 0;
        prevHipHopSelectIndex = index;

        if (characterSelectStereod)
            return Play(hiphopSelectData[index], isP1);
        else
            return Play(hiphopSelectData[index]);
    }
    public SoundBuilder SmallCheer()
    {
        if (smallCheerData.Count == 0) return null;
        int index = Random.Range(0, smallCheerData.Count - 1);
        index = index == prevSmallCheerIndex ? smallCheerData.Count - 1 : index;
        if (index == -1) index = 0;
        prevSmallCheerIndex = index;

        return Play(smallCheerData[index]);
    }
    // ===================== GAMEPLAY =====================
    public SoundBuilder MoveRegistered(bool isP1)
    {
        if (moveRegisteredStereod)
            return Play(moveRegisteredData, isP1);
        else
            return Play(moveRegisteredData);
    }
    public SoundBuilder MoveRegisteredOnBeat(bool isP1)
    {
        if (moveRegisteredOnBeatData.Count == 0) return null;
        int index = Random.Range(0, moveRegisteredOnBeatData.Count - 1);
        index = index == prevOnBeatBonusIndex ? moveRegisteredOnBeatData.Count - 1 : index;
        if (index == -1) index = 0;
        prevOnBeatBonusIndex = index;
        SoundData onBeatData = moveRegisteredOnBeatData[index];
        if (moveRegisteredStereod)
            return Play(onBeatData, isP1);
        else
            return Play(onBeatData);
    }
    public SoundBuilder TimerLow() => Play(timerLowData);
    public SoundBuilder TimerUp() => Play(timerUpData);

    // Meter
    public SoundBuilder MeterFilling(bool isP1) 
    {
        if (meterFillStereod)
            return Play(meterFillingData, isP1);
        else
            return Play(meterFillingData);
    }
    public SoundBuilder MeterAlmostFull(bool isP1) 
    {
        if (meterFillStereod)
            return Play(meterAlmostFullData, isP1);
        else
            return Play(meterAlmostFullData);
    }
    public SoundBuilder MeterOverflow(bool isP1) 
    {
        if (meterFillStereod)
            return Play(meterOverflowData, isP1);
        else
            return Play(meterOverflowData);
    }
    public SoundBuilder MeterLowPulse(bool isP1)
    {
        if (meterFillStereod)
            return Play(meterPulseLowData, isP1);
        else
            return Play(meterPulseLowData);
    }
    public SoundBuilder MeterMedPulse(bool isP1)
    {
        if (meterFillStereod)
            return Play(meterPulseMedData, isP1);
        else
            return Play(meterPulseMedData);
    }
    public SoundBuilder MeterHighPulse(bool isP1)
    {
        if (meterFillStereod)
            return Play(meterPulseHighData, isP1);
        else
            return Play(meterPulseHighData);
    }

    // Timing
    public SoundBuilder CountdownFinalBeats() => Play(countdownFinalBeatsData);

    public SoundBuilder SimpleStabs() 
    {
        if (simpleStabsData.Count == 0 || Time.time  - prevSimpleStabTimestamp < simpleStabDebounce) return null;
        prevSimpleStabTimestamp = Time.time;
        int index = Random.Range(0, simpleStabsData.Count - 1);
        index = index == prevSimpleStabIndex ? simpleStabsData.Count-1 : index;
        if (index == -1) index = 0;
        prevSimpleStabIndex = index;
        return Play(simpleStabsData[index]);
    }

    // ===================== UNIQUE MOVE SFX =====================
    public SoundBuilder LightAttackWin(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(lightAttackWinData, isP1);
        else
            return Play(lightAttackWinData);
    }
    public SoundBuilder HeavyAttackWin(bool isP1)
    {
        if (movesPlayedStereod)
            return Play(heavyAttackWinData, isP1);
        else
            return Play(heavyAttackWinData);
    }
    public SoundBuilder ChargeAttackWin(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(chargeAttackWinData, isP1);
        else
            return Play(chargeAttackWinData);
    }
    public SoundBuilder ParryAttackWin(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(parryAttackWinData, isP1);
        else
            return Play(parryAttackWinData);
    }

    public SoundBuilder LightAttackLose(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(lightAttackLoseData, isP1);
        else
            return Play(lightAttackLoseData);
    }
    public SoundBuilder HeavyAttackLose(bool isP1) 
    {
        if (movesPlayedStereod) 
            return Play(heavyAttackLoseData, isP1);
        else
            return Play(heavyAttackLoseData);
    }
    public SoundBuilder ChargeAttackLose(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(chargeAttackLoseData, isP1);
        else
            return Play(chargeAttackLoseData);
    }
    public SoundBuilder ParryAttackLose(bool isP1)
    {
        if (movesPlayedStereod)
            return Play(parryAttackLoseData, isP1);
        else
            return Play(parryAttackLoseData);
    }
    public SoundBuilder ParryAttackNeutral(bool isP1) 
    {
        if (movesPlayedStereod)
            return Play(parryAttackNeutralData, isP1);
        else
            return Play(parryAttackNeutralData);
    }
    public SoundBuilder DomainExpansion()
    {
        return Play(domainExpansionData);
    }
    public SoundBuilder LoudCheer()
    {
        return Play(loudCheerData);
    }
    public SoundBuilder TutorialDialogue()
    {
        if (tutorialDialogueData.Count == 0) return null;
        int index = Random.Range(0, tutorialDialogueData.Count - 1);
        index = index == prevTutorialDialogueIndex ? tutorialDialogueData.Count - 1 : index;
        if (index == -1) index = 0;
        Debug.Log("tutorialdialogue soundClip: " + tutorialDialogueData[index].clip.name);
        prevTutorialDialogueIndex = index;
        return Play(tutorialDialogueData[index]);
    }
    public SoundBuilder TutorialPrompt()
    {
        return Play(tutorialPromptData);
    }
    public SoundBuilder TutorialYesNo()
    {
        return Play(tutorialYesNoData);
    }
    public SoundBuilder Laser01()
    {
        return Play(laser01Data);
    }
    public SoundBuilder Laser02()
    {
        return Play(laser02Data);
    }
    public SoundBuilder Car()
    {
        return Play(carData);
    }
    public SoundBuilder BadInput()
    {
        return Play(badInputData);
    }
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