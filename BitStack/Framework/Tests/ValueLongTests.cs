using BitStack;
using NUnit.Framework;
using UnityEngine;

/**
 * Unit Tests designed to be ran by the Unity Test Runner which tests functionality
 * related to the long data type (signed long, 64 bits)
 */
public static class ValueLongTests {

    static readonly long TEST_VALUE = -396761530871789; // 1111111111111110100101110010010111000001111001000101000000010011
    static readonly string TEST_VALUE_STR = "1111111111111110100101110010010111000001111001000101000000010011";
    static readonly string TEST_HEX = "FFFE9725C1E45013";
    static readonly int LOOP_COUNT = 64;

    // the expected bit sequence in array form
    static readonly int[] EXPTECTED_BITS = { 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    static readonly byte[] EXPECTED_BYTES = { 127, 246, 0, 187, 192, 5, 18, 57 };

    [Test]
    public static void Test_BitAt() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.BitAt(i) == EXPTECTED_BITS[i]);
        }
    }

    [Test]
    public static void Test_BitInvAt() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.BitInvAt(i) != (EXPTECTED_BITS[i]));
        }
    }

    [Test]
    public static void Test_Bool() {
        long posValue = 2;
        long zeroValue = 0;
        long negValue = -2;

        Debug.Assert(posValue.Bool());
        Debug.Assert(!zeroValue.Bool());
        Debug.Assert(!negValue.Bool());
    }

    [Test]
    public static void Test_SetBitAt() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.SetBitAt(i).BitAt(i) == 1, "Expected Bit Position(" + i + ") to be 1");
        }
    }

    [Test]
    public static void Test_UnsetBitAt() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.UnsetBitAt(i).BitAt(i) == 0, "Expected Bit Position(" + i + ") to be 0");
        }
    }

    [Test]
    public static void Test_SetBit() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.SetBit(i, 0).BitAt(i) == 0, "Expected Bit Position(" + i + ") to be 0");
            Debug.Assert(TEST_VALUE.SetBit(i, 1).BitAt(i) == 1, "Expected Bit Position(" + i + ") to be 1");
        }
    }

    [Test]
    public static void Test_SetUnsetBit() {
        for (int i = 0; i < LOOP_COUNT; i++) {
            Debug.Assert(TEST_VALUE.SetBit(i, 0).SetBit(i, 1).BitAt(i) == 1, "Expected Bit Position(" + i + ") to be 1");
            Debug.Assert(TEST_VALUE.SetBit(i, 1).SetBit(i, 0).BitAt(i) == 0, "Expected Bit Position(" + i + ") to be 0");
        }
    }

    [Test]
    public static void Test_ToggleBitAt() {
        long inv = TEST_VALUE;
        var invTest = ~TEST_VALUE;

        for (int i = 0; i < LOOP_COUNT; i++) {
            inv = inv.ToggleBitAt(i);
        }

        Debug.Assert(inv == invTest, "Expected Toggle(" + inv + ") and InvTest(" + (invTest) + ") to Match.");

        // invert the order to ensure
        for (int i = 0; i < LOOP_COUNT; i++) {
            inv = inv.ToggleBitAt(i);
        }

        Debug.Assert(inv == TEST_VALUE, "Expected Toggle(" + inv + ") and Test(" + TEST_VALUE + ") to Match.");
    }

    [Test]
    public static void Test_PopCount() {
        var popCount = TEST_VALUE.PopCount();

        Debug.Assert(TEST_VALUE.PopCount() == 35, "Expected Value(" + popCount + ") and Test(35) + to Match");
    }

    [Test]
    public static void Test_BitString() {
        var testStr = TEST_VALUE.BitString();

        Debug.Assert(testStr == TEST_VALUE_STR, "Expected String(" + testStr + ") and Test(" + TEST_VALUE_STR + ") to Match.");
    }

    [Test]
    public static void Test_LongFromBitString() {
        var testLong = TEST_VALUE_STR.LongFromBitString();

        Debug.Assert(testLong == TEST_VALUE, "Expected Int(" + testLong + ") and Test(" + TEST_VALUE + ") to Match.");
    }

    [Test]
    public static void Test_HexString() {
        var testHex = TEST_VALUE.HexString();

        Debug.Assert(testHex == TEST_HEX, "Expected Hex(" + testHex + ") and Test(" + TEST_HEX + ") to Match.");
    }

    [Test]
    public static void Test_IsPowerOfTwo() {
        long pow1 = 1024;
        long pow2 = 2048;
        long pow3 = 4096;
        long nPow1 = 1400;
        long nPow2 = 3300;
        long nPow3 = 5010;

        Debug.Assert(pow1.IsPowerOfTwo(), "Expected Test(" + pow1 + ") To be Power of Two");
        Debug.Assert(pow2.IsPowerOfTwo(), "Expected Test(" + pow2 + ") To be Power of Two");
        Debug.Assert(pow3.IsPowerOfTwo(), "Expected Test(" + pow3 + ") To be Power of Two");
        Debug.Assert(!nPow1.IsPowerOfTwo(), "Expected Test(" + nPow1 + ") To be Non Power of Two");
        Debug.Assert(!nPow2.IsPowerOfTwo(), "Expected Test(" + nPow2 + ") To be Non Power of Two");
        Debug.Assert(!nPow3.IsPowerOfTwo(), "Expected Test(" + nPow3 + ") To be Non Power of Two");
    }

    [Test]
    public static void Test_ByteTuple() {
        var tuple = TEST_VALUE.SplitIntoByte();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_SByteTuple() {
        var tuple = TEST_VALUE.SplitIntoSByte();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_ShortTuple() {
        var tuple = TEST_VALUE.SplitIntoShort();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_UShortTuple() {
        var tuple = TEST_VALUE.SplitIntoUShort();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_IntTuple() {
        var tuple = TEST_VALUE.SplitIntoInt();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_UIntTuple() {
        var tuple = TEST_VALUE.SplitIntoUInt();

        var revert = tuple.CombineToLong();
        Debug.Assert(revert == TEST_VALUE, "Expected Test(" + revert + ") To be equal to Value(" + TEST_VALUE + ")");
    }

    [Test]
    public static void Test_GetByte() {
        var tuple = TEST_VALUE.SplitIntoByte();

        byte[] test_data = { tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item1.Item3, tuple.Item1.Item4, tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item2.Item3, tuple.Item2.Item4 };

        for (int i = 0; i < 8; i++) {
            byte testValue = TEST_VALUE.ByteAt(i);
            Debug.Assert(test_data[i] == testValue, "Expected Test(" + test_data[i] + ") To be equal to Value(" + testValue + ")");
        }
    }

    [Test]
    public static void Test_SetByteAt() {
        var tuple = TEST_VALUE.SplitIntoByte();

        byte[] test_data = { tuple.Item1.Item1, tuple.Item1.Item2, tuple.Item1.Item3, tuple.Item1.Item4, tuple.Item2.Item1, tuple.Item2.Item2, tuple.Item2.Item3, tuple.Item2.Item4 };

        for (int i = 0; i < (LOOP_COUNT / 8); i++) {
            long newValue = TEST_VALUE.SetByteAt(EXPECTED_BYTES[i], i);
            var new_tuple = newValue.SplitIntoByte();
            byte[] new_test_data = { new_tuple.Item1.Item1, new_tuple.Item1.Item2, new_tuple.Item1.Item3, new_tuple.Item1.Item4, new_tuple.Item2.Item1, new_tuple.Item2.Item2, new_tuple.Item2.Item3, new_tuple.Item2.Item4 };
            Debug.Assert(new_test_data[i] == EXPECTED_BYTES[i], "Expected First Test(" + new_test_data[i] + ") To be equal to Value(" + EXPECTED_BYTES[i] + ")");

            // perform the inverse operation - this is to ensure the old value
            // can be placed over the new value properly
            newValue = newValue.SetByteAt(test_data[i], i);
            new_tuple = newValue.SplitIntoByte();
            new_test_data = new byte[] { new_tuple.Item1.Item1, new_tuple.Item1.Item2, new_tuple.Item1.Item3, new_tuple.Item1.Item4, new_tuple.Item2.Item1, new_tuple.Item2.Item2, new_tuple.Item2.Item3, new_tuple.Item2.Item4 };
            Debug.Assert(new_test_data[i] == test_data[i], "Expected Second Test(" + new_test_data[i] + ") To be equal to Value(" + test_data[i] + ")");
        }
    }
}