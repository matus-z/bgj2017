using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    [Tooltip("Timer object")]
    public GameObject DayProgressTimer;

    public GameObject GameplayScreen;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject NightScreen;

    private float DayTimeRemaining;

    public ActionCard ActionCardSelected { get; private set; }

    public int RoomsRows = 5;
    public int RoomsCols = 5;

    public Room RoomPrefab;

    private List<Room> Rooms;

    private enum EState
    {
        SelectActionCard,
        SelectRoom
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    private void Start ()
    {
        DayTimeRemaining = DayLength;

        Rooms = new List<Room>();

        for (int row = 0; row < RoomsRows; row++)
        {
            for (int col = 0; col < RoomsRows; col++)
            {
                Vector3 pos = new Vector3(-2 + row, -2 + col, 1);
                Room room = Instantiate(RoomPrefab, pos, Quaternion.identity);
                room.Row = row;
                room.Col = col;
                Rooms.Add(room);
            }
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void Update ()
    {
        DayTimeRemaining -= Time.deltaTime;
        if (DayTimeRemaining < 0)
        {
            EndDay();
            return;
        }

        if (!DayProgressTimer) return;
        Image img = DayProgressTimer.GetComponent<Image>();
        if (!img) return;

        img.fillAmount = DayTimeRemaining / DayLength;
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    public void SelectActionCard(ActionCard actionCard)
    {
        if (GetState() != EState.SelectActionCard) return;
        if (!actionCard) return;

        ActionCardSelected = actionCard;
        ActionCardSelected.SetSelected(true);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectRoom(Room room)
    {
        if (GetState() != EState.SelectRoom) return;
        if (!room) return;
        if (!ActionCardSelected) return;

        Debug.Log(ActionCardSelected.ApplyTo);
        switch (ActionCardSelected.ApplyTo)
        {
            case ActionCard.EApplyTo.Room: ApplyActionTo(room.Row, room.Col); break;
            case ActionCard.EApplyTo.Row: ApplyActionTo(room.Row, null); break;
            case ActionCard.EApplyTo.Col: ApplyActionTo(null, room.Col); break;
            case ActionCard.EApplyTo.Global: ApplyActionTo(null, null); break;
        }

        ActionCardSelected.SetSelected(false);
        ActionCardSelected = null;
        SolveWinLose();
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ApplyActionTo(int? row, int? col)
    {
        foreach (Room room in Rooms)
        {
            if (row.HasValue && row.Value != room.Row) continue;
            if (col.HasValue && col.Value != room.Col) continue;

            ActionCardSelected.ApplyAction(room);
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private EState GetState()
    {
        return ActionCardSelected ? EState.SelectRoom : EState.SelectActionCard;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void EndDay()
    {
        DayTimeRemaining = DayLength;

        if (ActionCardSelected) ActionCardSelected.SetSelected(false);

        ActionCardSelected = null;

        System.Random rnd = new System.Random();
        int roomsToDarken = 10;
        while (roomsToDarken > 0)
        {
            int r = rnd.Next(Rooms.Count);
            //bool hasDarkened = 
            Rooms[r].DarkenRoomState();
            //if (hasDarkened)
            roomsToDarken--;
        }

        SolveWinLose();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void SolveWinLose()
    {
        var RoomsClean = Rooms.Where(room => room.RoomState == Room.ERoomState.Clean).ToList();
        if (RoomsClean.Count == Rooms.Count)
        {
            GameplayScreen.SetActive(false);
            WinScreen.SetActive(true);
            LoseScreen.SetActive(false);
            NightScreen.SetActive(false);

            foreach (Room room in Rooms) Destroy(room.gameObject);
            Rooms.Clear();
            return;
        }

        var RoomsHeadGear = Rooms.Where(room => room.RoomState == Room.ERoomState.HeadGear).ToList();
        if (RoomsHeadGear.Count == Rooms.Count)
        {
            GameplayScreen.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(true);
            NightScreen.SetActive(false);

            foreach (Room room in Rooms) Destroy(room.gameObject);
            Rooms.Clear();
            return;
        }
    }
}
