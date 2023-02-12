using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
namespace Day25Challenge
{
    class PureSNAFU
    {
        // The elves made a terrible mistake! The software to add snafu numbers did not work on the SNAFU hardware because it was unfamiliar with integers as we know them.
        // They required software that does not use integers at all! Luckely, we created pure SNAFU:
        // pure SNAFU additions work like an ODO meter: the characters can move both ways, and if one goes from '=' to '2' the character to the left is lowered by one. Vice versa,
        // if a character goes from '2' to '=', the next character is incremented by one.

        // The data structures resemble linked lists: Each character is a looped linked list of nodes with characters '=', '-', '0', '-1', '-2'. This class is called SNAFUcharacterWheel.
        // The SNAFU itself is a linked list of SNAFU character wheels. This is usefull because if a snafu character wheel goes from = to 2 and vice versa, the next wheel is moved by 1.

        public SNAFUcharacterWheel active = new();

        public PureSNAFU(string snafu)
        {
            AddOtherSNAFUnumber(snafu);
        }

        public void AddOtherSNAFUnumber(string SNAFU)
        {
            MoveToRightMostSymbol();
            foreach (char c in SNAFU.Reverse())
            {
                ProcessChar(c);
                if (active.nextSnafuSymbol == null)
                {
                    active.nextSnafuSymbol = new() { previousSnafuSymbol = active };
                }
                active = active.nextSnafuSymbol;
            }
            // this can result in the snafu number having '0' as a leftmost symbol, remove if there are more symbols:
            if (active.nextSnafuSymbol == null && active.previousSnafuSymbol != null && active.activeCharacter.snafuChar == '0')
            {
                active = active.previousSnafuSymbol;
                active.nextSnafuSymbol = null;
            }
        }

        private void MoveToRightMostSymbol()
        {
            while (active.previousSnafuSymbol != null)
            {
                active = active.previousSnafuSymbol;
            }
        }

        public string ReturnAsString()
        {
            string result = "";
            // move to left of the SNAFU symbol sequence:
            SNAFUcharacterWheel? tempActive = active;
            while (tempActive.nextSnafuSymbol != null)
            {
                tempActive = tempActive.nextSnafuSymbol;
            }
            // add characters one by one:
            while (tempActive != null)
            {
                result += tempActive.activeCharacter.snafuChar;
                tempActive = tempActive.previousSnafuSymbol;
            }
            return result;
        }

        private void ProcessChar(char SNAFUsymbol)
        {
            switch (SNAFUsymbol)
            {
                case '2':
                    active.Next();
                    active.Next();
                    break;
                case '1':
                    active.Next();
                    break;
                case '-':
                    active.Previous();
                    break;
                case '=':
                    active.Previous();
                    active.Previous();
                    break;
                default:
                    break;
            }
        }
    };

    class SNAFUcharacterWheel
    {
        // The snafu character wheel is a looped linked listed with next and previous operators: 
        //  |--> '=' <->  '='  <--> '0'  <--> '1'  <--> '2' <--|
        //  |--------------------------------------------------|
        // one of the nodes is active.
        public SNAFUcharacterWheel? nextSnafuSymbol;
        public SNAFUcharacterWheel? previousSnafuSymbol;
        public SNAFUnode activeCharacter = new('0');
        public SNAFUcharacterWheel()
        {
            SNAFUnode nodeMinus = new('=');
            SNAFUnode nodeMinusOne = new('-');
            SNAFUnode nodePlus = new('1');
            SNAFUnode nodePlusTwo = new('2');

            nodeMinus.previous = nodePlusTwo;
            nodeMinus.next = nodeMinusOne;
            nodeMinusOne.previous = nodeMinus;
            nodeMinusOne.next = activeCharacter;
            activeCharacter.previous = nodeMinusOne;
            activeCharacter.next = nodePlus;
            nodePlus.previous = activeCharacter;
            nodePlus.next = nodePlusTwo;
            nodePlusTwo.previous = nodePlus;
            nodePlusTwo.next = nodeMinus;
        }

        public void Next()
        {
            activeCharacter = activeCharacter.next;
            if (activeCharacter.snafuChar == '=') // The next SNAFU character wheel is triggered to go to the next character if we made a full rotation:
            {
                if (nextSnafuSymbol == null)
                {
                    nextSnafuSymbol = new() { previousSnafuSymbol = this };
                }
                nextSnafuSymbol.Next();
            }
        }

        public void Previous()
        {
            activeCharacter = activeCharacter.previous;
            if (activeCharacter.snafuChar == '2') // The next SNAFU character wheel is triggered to go to the previous character if we made a full rotation backwards:
            {
                if (nextSnafuSymbol == null)
                {
                    nextSnafuSymbol = new() { previousSnafuSymbol = this };
                }
                nextSnafuSymbol.Previous();
            }
        }
    };

    class SNAFUnode
    {
        public char snafuChar;
        public SNAFUnode? previous = null;
        public SNAFUnode? next = null;
        public SNAFUnode(char c)
        {
            snafuChar = c;
        }
    }
}
