using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TenTips : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        // LESSON 1
        // Instead of this, where we can lose




    }

    /// <summary>
    /// How to avoid race conditions (other threads or routines fucking with
    /// our variables before we use our result), by doing it all in one line
    /// </summary>
    public void ShowExample1() {
        Int32 result = new Int32();
        Int32 calculatedTotal = 0;
        var d1 = new Dictionary<string, int>();
        d1.Add("ten", 10);
        d1.Add("twenty", 20);
        d1.Add("thirty", 30);

        // BAD Code
        if (d1.ContainsKey("thirty")) {
            // Another thread could have removed this before this code runs
            result = d1["thirty"];

            // do more work here
            calculatedTotal = result * 5;

        }

        // USE THIS CODE
        if (d1.TryGetValue("twenty", out result)) {
            calculatedTotal = result * 5;
            Debug.Log("Result: " + result);
        } else {
            Debug.Log("Key does not exist");
        }


    }


    public void ShowExample2() {

        //SwitchOldSyntax();
        //SwitchOnType();
        SwitchWithWhen();

    }

    void SwitchOldSyntax() {

        int number = 23;

        switch (number) {
            case 0:

                break;

            case 10:
                break;
            case 20:
                break;
            case 23:
                Debug.Log("23");
                break;
        }

    }

    void SwitchOnType() {


    }

    /// <summary>
    /// Pattern matching with switch  statements with when
    /// </summary>
    void SwitchWithWhen() {
        var numbers = Enumerable.Range(1, 110);
        string message = string.Empty;
        var eighties = new List<int> { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 };

        foreach (var number in numbers) {
            message = string.Empty;

            switch (number) {
                case 42:
                    message += "Meaning of Life,";
                    break;
                case var candidate when (candidate % 20 == 0):
                    message += "Mod 20,";
                    break;
                case var candidate when (eighties.Contains(candidate)):
                    message += "Eighties,";
                    break;
                default:
                    break;
            }

        }

        Debug.Log(message);

    }

    void ShowExample3() {
        long numA = 222_333_444_555; // using underscores in large numbers can be used like commas for 1K

        Debug.Log(numA);
    }

    void ShowExample4() {

        // This is a "discard", temp variables intentionally unused
        // Can be useful when you need to satisfy the compiler while not using the value
        _ = 12 + 14;

        DiscardOutParam();

    }

    /// <summary>
    /// "It seems a waste to declare a variable if I'm not going to use it"
    /// </summary>
    void DiscardOutParam() {
        var inputs = new List<string> { "123", "234", "abc", "def" };


        foreach (string input in inputs) {

            // We use our discard here to satisfy an output that we'll never use
            if (int.TryParse(input, out _)) {
                Debug.Log("Valid");
            } else {
                Debug.Log("Invalid");
            }

        }

    
    }

    void ShowExample5() {
        // The conditional operatior ?:, or the ternary conditional operator,
        // evaluates a Boolean expression and returns the result of one of the two expressions
        

    }

}
