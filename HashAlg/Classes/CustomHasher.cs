namespace HashAlg.Classes;

using System;
using System.Text;

public static class CustomHasher
{
    private const int HashLength = 30;

    public static string GetFileHash(string filePath)
    {
        try
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                return ByteArrayToHexString(ComputeHash(fs));
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public static string GetStringHash(string input)
    {
        return ByteArrayToHexString(ComputeHash(input));
    }

    private static byte[] ComputeHash(dynamic input)
    {
        byte[] sourceBytes;

        if (input is string)
        {
            sourceBytes = GetEnlargedByteArray(Encoding.ASCII.GetBytes((string)input));
        }
        else if (input is FileStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                ((FileStream)input).CopyTo(memoryStream);
                sourceBytes = GetEnlargedByteArray(memoryStream.ToArray());
            }
        }
        else
        {
            throw new ArgumentException("Invalid input type. Expected string or FileStream.");
        }

        byte[] result = new byte[HashLength / 2];

        for (int i = 0; i < HashLength / 2; i++)
        {
            for (int j = 0; j < sourceBytes.Length; j++)
            {
                if ((j + i) % 2 == 0)
                    result[i] = (byte)~(sourceBytes[i] ^ 0xAF | sourceBytes[j] & 0xCD << 0x1D);
                else
                    result[i]=(byte)~(sourceBytes[i] ^ sourceBytes[j]);
            }
        }

        return result;
    }

    private static byte[] GetEnlargedByteArray(byte[] inputBytes)
    {
        if (inputBytes.Length >= HashLength / 2)
            return inputBytes;

        byte[] doubledBytes = new byte[inputBytes.Length * 2];
        Array.Copy(inputBytes, doubledBytes, inputBytes.Length);

        return GetEnlargedByteArray(doubledBytes);
    }

    private static string ByteArrayToHexString(byte[] arrInput)
    {
        StringBuilder hexString = new StringBuilder(arrInput.Length);

        for (int i = 0; i < arrInput.Length; i++)
        {
            hexString.Append(arrInput[i].ToString("X2"));
        }

        return hexString.ToString();
    }
}