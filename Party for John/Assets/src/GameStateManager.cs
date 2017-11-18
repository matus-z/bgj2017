using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    public float NightLength = 1;

    [Tooltip("Count of days until game over")]
    public int DaysUntilApocalypse = 3;

    [Tooltip("Timer object")]
    public GameObject DayProgressTimer;

    public GameObject GameplayScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject NightScreen;

    private float TimeRemaining;
    private int DaysRemaining;

    public ActionCard ActionCardSelected { get; private set; }

    public int RoomsRows = 4;
    public int RoomsCols = 4;

    public Room RoomPrefab;

    private List<Room> Rooms;

    private enum EState
    {
        SelectActionCard,
        SelectRoom
    }

    private enum EDayNight
    {
        Day,
        Night
    }

    private EDayNight DayOrNight = EDayNight.Day;

	private SoundManager snd;

    // ------------------------------------------------------------------------------------------------------------------
    private void Start()
    {
        TimeRemaining = DayLength;
        DaysRemaining = DaysUntilApocalypse;

        Rooms = new List<Room>();
		snd = GameObject.Find ("AudioManager").GetComponent <SoundManager> ();

        System.Random rng = new System.Random();
        int rotation;

        for (int row = 0; row < RoomsRows; row++)
        {
            for (int col = 0; col < RoomsRows; col++)
            {
                Vector3 pos = new Vector3(
                    -1.75f + row + ((row >= RoomsRows / 2) ? 0.5f : 0),
                    -1.75f + col + ((col >= RoomsCols / 2) ? 0.5f : 0),
                    1);

                Room room = Instantiate(RoomPrefab, pos, Quaternion.identity);
                room.Row = row;
                room.Col = col;
				room.transform.SetParent(transform.parent);
                rotation = -90 * rng.Next(2);
                room.Rotate(rotation);
                Rooms.Add(room);
            }
        }

        UpdateDaysRemainingText();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining < 0)
        {
            switch (DayOrNight)
            {
                case EDayNight.Day: EndDay(); return;
                case EDayNight.Night: EndNight(); return;
            }
        }

        if (!DayProgressTimer) return;
        Image img = DayProgressTimer.GetComponent<Image>();
        if (!img) return;

        img.fillAmount = TimeRemaining / DayLength;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectActionCard(ActionCard actionCard)
    {
        if (GetState() != EState.SelectActionCard) return;
        if (!actionCard) return;

        ActionCardSelected = actionCard;
        ActionCardSelected.SetSelected(true);
		snd.PlaySound ("click");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectRoom(Room room)
    {
        if (GetState() != EState.SelectRoom) return;
        if (!room) return;
        if (!ActionCardSelected) return;

        switch (ActionCardSelected.Type)
        {
            case ActionCard.EActionType.FacebookStatus: ActionFacebookStatus(); break;
            case ActionCard.EActionType.PhoneCall: ActionPhoneCall(); break;
            case ActionCard.EActionType.EMP: ActionEMP(); break;
            case ActionCard.EActionType.ScreamOfTruth: ActionScreamOfTruth(); break;
            case ActionCard.EActionType.PersonalVisit: ActionPersonalVisit(); break;
        }
        
        ActionCardSelected.SetSelected(false);
        ActionCardSelected = null;
        SolveWin();
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    public void ActionFacebookStatus()
    {
        var rPhone = AllInState(Room.ERoomState.Phone);
        foreach (Room r in rPhone) ActionCardSelected.ApplyChange(r, -1);

        var rPc = AllInState(Room.ERoomState.Pc);
        foreach (Room r in rPc) ActionCardSelected.ApplyChange(r, RandBool50Perc() ? - 1 : 1);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionPhoneCall()
    {
        // TODO
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionEMP()
    {
        var rHg = AllInState(Room.ERoomState.HeadGear);
        foreach (Room r in rHg) ActionCardSelected.ApplyChange(r, RandBool50Perc() ? -1 : -2);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionScreamOfTruth()
    {
        // TODO
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionPersonalVisit()
    {
        // TODO
    }

    // ------------------------------------------------------------------------------------------------------------------
    private EState GetState()
    {
        return ActionCardSelected ? EState.SelectRoom : EState.SelectActionCard;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private bool IsGameEnd()
    {
        return Rooms.Count == 0;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void EndDay()
    {
        if (IsGameEnd()) return;

        DaysRemaining--;
        UpdateDaysRemainingText();

        if (ActionCardSelected) ActionCardSelected.SetSelected(false);

        ActionCardSelected = null;

        System.Random rnd = new System.Random();
        int roomsToDarken = 10;
        while (roomsToDarken > 0)
        {
            int r = rnd.Next(Rooms.Count);
            Rooms[r].ChangeRoomState(1);
            roomsToDarken--;
        }

        bool isWinOrLose = SolveLose();
        if (isWinOrLose) return;

        DayOrNight = EDayNight.Night;
        TimeRemaining = NightLength;

        GameplayScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        NightScreen.SetActive(true);
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void EndNight()
    {
        if (IsGameEnd()) return;

        DayOrNight = EDayNight.Day;
        TimeRemaining = DayLength;

        GameplayScreen.SetActive(true);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        NightScreen.SetActive(false);
    }

    // ------------------------------------------------------------------------------------------------------------------
    private bool SolveWin()
    {
        var rCl = AllInState(Room.ERoomState.Clean);
        if (rCl.Count == Rooms.Count)
        {
            GameplayScreen.SetActive(false);
            WinScreen.SetActive(true);
            LoseScreen.SetActive(false);
            NightScreen.SetActive(false);

            foreach (Room room in Rooms) Destroy(room.gameObject);
            Rooms.Clear();
            return true;
        }
        
        return false;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private bool SolveLose()
    {
        var rHg = AllInState(Room.ERoomState.HeadGear);
        if (rHg.Count == Rooms.Count || DaysRemaining < 0)
        {
            GameplayScreen.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(true);
            NightScreen.SetActive(false);

            foreach (Room room in Rooms) Destroy(room.gameObject);
            Rooms.Clear();
            return true;
        }

        return false;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void UpdateDaysRemainingText()
    {
        GameObject goDrt = GameObject.Find("DaysRemainingText");
        if (!goDrt) return;

        Text txt = goDrt.GetComponent<Text>();
        if (!txt) return;

        txt.text = DaysRemaining.ToString();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private List<Room> AllInState(Room.ERoomState rs)
    {
        return Rooms.Where(room => room.RoomState == rs).ToList();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private bool RandBool50Perc()
    {
        System.Random rng = new System.Random();
        Debug.Log(rng.Next(2));
        return rng.Next(2) >= 1;
    }
}
