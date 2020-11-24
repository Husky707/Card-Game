using System.Collections.Generic;
using System;

public enum DeckPosition { Top, Bottom, Random}
public class Deck <T> where T : Card
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

    public T GetCard(int atindex)
    {
        if (!IndexInRange(atindex) || _cards[atindex] == null)
            return default;

        return _cards[atindex];
    }

    public T Draw(DeckPosition fromPos = DeckPosition.Top)
    {
        if(IsEmpty)
        {
            UnityEngine.Debug.LogWarning("Deck is empty.");
            return default;
        }

        int targeti = GetIndexFromPosistion(fromPos);
        T removedCard = _cards[targeti];
        Remove(targeti);

        return removedCard;
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


    public void Remove(T target)
    {
        Remove(GetIndexOfTarget(target));
    }
    public void Remove(int atIndex)
    {
        if(IsEmpty)
        {
            UnityEngine.Debug.LogWarning("Cannot remove a card from empty deck.");
            return;
        }
        else if(!IndexInRange(atIndex))
        {
            UnityEngine.Debug.LogWarning("Cannot remove card from deck. Index out of bounds.");
            return;
        }

        T removedCard = _cards[atIndex];
        _cards.RemoveAt(atIndex);

        CardRemoved.Invoke(removedCard);

        if (_cards.Count == 0)
            Emptied.Invoke();

    }

    public void ShuffleDeck()
    {
        for(int i = Count - 1; i > 0; i--)
        {
            int randi = UnityEngine.Random.Range(0, i + 1);
            T randCard = _cards[randi];

            _cards[randi] = _cards[i];
            _cards[i] = randCard;
        }
    }

    #region Helpers
    private bool IndexInRange(int indexCheck)
    {
        if(_cards.Count > indexCheck && indexCheck >= 0)
            return true;

        UnityEngine.Debug.LogWarning("Index " + indexCheck + " out side of list range(" + _cards.Count + ")");
        return false;
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

    private int GetIndexOfTarget(T target)
    {
        if (Count == 0)
            return 0;

        if (target == null)
            return 0;

        if (!_cards.Contains(target))
        {
            UnityEngine.Debug.Log("Deck does not contain the targeted card");
            return -1;
        }

        return _cards.IndexOf(target);
    }
    #endregion
}
