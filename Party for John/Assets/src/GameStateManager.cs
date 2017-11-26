using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenShake))]
public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    [Tooltip("Count of days until game over")]
    public int DaysUntilApocalypse = 3;

    [Tooltip("Timer object")]
    public GameObject DayProgressTimer;

    public GameObject GameplayScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject NightScreen;
	public GameObject backObject;

    public Room RoomPrefab;

    [Tooltip("Rows in the grid")]
    public int RoomsRows = 4;

    [Tooltip("Cols in the grid")]
    public int RoomsCols = 4;

    private float TimeRemaining;
    private int DaysRemaining;

    public ActionCard ActionCardSelected { get; private set; }

    private List<Room> Rooms;

    public enum EDayNight
    {
        Day,
        Night
    }

    private EDayNight DayOrNight = EDayNight.Day;

	private SoundManager snd;

    // ------------------------------------------------------------------------------------------------------------------
    public void StartGame()
    {
        TimeRemaining = DayLength;
        DaysRemaining = DaysUntilApocalypse;
		GameplayScreen.SetActive (true);
        Rooms = new List<Room>();
		snd = GameObject.Find ("AudioManager").GetComponent <SoundManager> ();

        System.Random rng = new System.Random();
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
                int rotation = -90 * rng.Next(2);
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
        if (TimeRemaining < 0 && EDayNight.Day == DayOrNight)
        {
            EndDay();
            return;
        }

        if (!DayProgressTimer) return;
        Image img = DayProgressTimer.GetComponent<Image>();
        if (!img) return;

        img.fillAmount = TimeRemaining / DayLength;
		UpdateDaysRemainingText();
    }

    // ------------------------------------------------------------------------------------------------------------------
	public bool IsDay()
    {
        return (DayOrNight == EDayNight.Day);
	}

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectActionCard(ActionCard actionCard)
    {
        if (ActionCardSelected) ActionCardSelected.SetSelected(false);
        if (!actionCard) return;

        ActionCardSelected = actionCard;
        ActionCardSelected.SetSelected(true);
		snd.PlaySound ("click");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectRoom(Room room)
    {
        if (!ActionCardSelected) return;
        if (!room) return;
        if (!ActionCardSelected) return;

        bool isWrongRoom = true;
        switch (ActionCardSelected.Type)
        {
            case ActionCard.EActionType.FacebookStatus: isWrongRoom = (room.RoomState != Room.ERoomState.Pc && room.RoomState != Room.ERoomState.Phone); break;
            case ActionCard.EActionType.PhoneCall: isWrongRoom = (room.RoomState != Room.ERoomState.Phone); break;
            case ActionCard.EActionType.EMP: isWrongRoom = (room.RoomState != Room.ERoomState.HeadGear); break;
            case ActionCard.EActionType.ScreamOfTruth: isWrongRoom = (room.RoomState != Room.ERoomState.Clean); break;
		case ActionCard.EActionType.PersonalVisit: isWrongRoom = false; break;
        }

        if (isWrongRoom)
        {
            ClickOnIncorrectRoom();
        }
        else
        {
            switch (ActionCardSelected.Type)
            {
                case ActionCard.EActionType.FacebookStatus: ActionFacebookStatus(room); break;
                case ActionCard.EActionType.PhoneCall: ActionPhoneCall(room); break;
                case ActionCard.EActionType.EMP: ActionEMP(room); break;
                case ActionCard.EActionType.ScreamOfTruth: ActionScreamOfTruth(room); break;
                case ActionCard.EActionType.PersonalVisit: ActionPersonalVisit(room); break;
            }

			ActionCardSelected.SetSelected(false);
			ActionCardSelected = null;
			SolveWin();
        }
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    public void ActionFacebookStatus(Room room)
    {
        var rPhone = AllInState(Room.ERoomState.Phone);
        foreach (Room r in rPhone) ActionCardSelected.ApplyChange(r, -1);

        var rPc = AllInState(Room.ERoomState.Pc);
        foreach (Room r in rPc) ActionCardSelected.ApplyChange(r, RandBool(0.5f) ? - 1 : 1);
		snd.PlaySound ("keyboard");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionPhoneCall(Room room)
    {
        ActionCardSelected.ApplyChange(room, -1);
		snd.PlaySound ("phone");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionEMP(Room room)
    {
        var rHg = AllInState(Room.ERoomState.HeadGear);
        foreach (Room r in rHg) ActionCardSelected.ApplyChange(r, RandBool(0.7f) ? -1 : -2);

        System.Random rng = new System.Random();
        int ran = rng.Next(Rooms.Count);

        ActionCardSelected.ApplyChange(Rooms[ran], 100);
		snd.PlaySound ("emp");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionScreamOfTruth(Room room)
    {
        int quadRow = room.Row / 2;
        int quadCol = room.Col / 2;

        foreach (Room r in Rooms)
            if (r.Row / 2 == quadRow && r.Col / 2 == quadCol)
                ActionCardSelected.ApplyChange(r, -100);

		snd.PlaySound ("scream");
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ActionPersonalVisit(Room room)
    {
        ActionCardSelected.ApplyChange(room, RandBool(0.2f) ? 0 : -1);
		snd.PlaySound ("doorbell");
    }

    // ------------------------------------------------------------------------------------------------------------------
    private bool IsGameEnd()
    {
        if (Rooms == null) return false;

        return Rooms.Count == 0;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void SetActive()
    {
        this.SetActive ();
		GameplayScreen.SetActive (true);
		WinScreen.SetActive(false);
		LoseScreen.SetActive(false);
		NightScreen.SetActive(true);
	}

    // ------------------------------------------------------------------------------------------------------------------
    private void EndDay()
    {
        if (IsGameEnd()) return;

        DaysRemaining--;
        UpdateDaysRemainingText();
		Background bg = backObject.GetComponent<Background> ();

        if (ActionCardSelected) ActionCardSelected.SetSelected(false);

        ActionCardSelected = null;

        System.Random rnd = new System.Random();
        int roomsToDarken = 10;
        while (roomsToDarken > 0 && Rooms != null)
        {
            int r = rnd.Next(Rooms.Count);
            Rooms[r].ChangeRoomState(1);
            roomsToDarken--;
        }

        bool isWinOrLose = SolveLose();
        if (isWinOrLose) return;

        DayOrNight = EDayNight.Night;
        bg.SetSprite(DayOrNight);

        //GameplayScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        NightScreen.SetActive(true);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void EndNight()
    {
        if (IsGameEnd()) return;

		Background bg = backObject.GetComponent<Background> ();

        DayOrNight = EDayNight.Day;
        bg.SetSprite(DayOrNight);

        TimeRemaining = DayLength;

        //GameplayScreen.SetActive(true);
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
        if (Rooms == null) return false;

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
        if (Rooms == null) return new List<Room>();

        return Rooms.Where(room => room.RoomState == rs).ToList();
    }

    // ------------------------------------------------------------------------------------------------------------------
	private bool RandBool(float Chance)
    {
        return UnityEngine.Random.value <= Chance;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void ClickOnIncorrectRoom()
    {
        snd.PlaySound("error");
		ActionCardSelected.Shake ();
    }
}
