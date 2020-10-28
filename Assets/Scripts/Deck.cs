using System.Collections.Generic;
using System;

public enum DeckPosition { Top, Bottom, Random}
public class Deck <T>
{

    List<T> _cards = new List<T>();

    public event Action Emptied = delegate { };
    public event Action<T> CardAdded = delegate { };
    public event Action<T> CardRemoved = delegate { };

    public int Count => _cards.Count;
    public T TopItem => _cards[_cards.Count - 1];
    public T BottomItem => _cards[0];
    public bool IsEmpty => _cards.Count == 0;
    public int Topi
    {
        get
        {
            if (_cards.Count == 0)
                return 0;
            else
                return _cards.Count - 1;
        }
    }


    public void Add(T card, DeckPosition atPos = DeckPosition.Top)
    {
        if (card == null) return;

        int insertIndex = GetIndexFromPosistion(atPos);
        if(insertIndex == Topi)
        {
            _cards.Add(card);
        }
        else
        {
            _cards.Insert(insertIndex, card);
        }

        CardAdded?.Invoke(card);
    }

    public void Add(List<T> cards, DeckPosition atPos = DeckPosition.Top)
    {
        foreach( T card in cards)
        {
            Add(card, atPos);
        }
    }

    private int GetIndexFromPosistion(DeckPosition position)
    {
        if (Count == 0)
            return 0;

        switch(position)
        {
            case DeckPosition.Top:
                return Topi;

            case DeckPosition.Bottom:
                return 0;

            case DeckPosition.Random:
                return UnityEngine.Random.Range(0, Topi + 1);

            default: UnityEngine.Debug.Log("Couldn't find deck position"); return -1;
        }
    }
}
