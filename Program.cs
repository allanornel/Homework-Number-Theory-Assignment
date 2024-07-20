using System.Numerics;
using System.Text;
using TeoriaDosNumerosTrab;

const int MIN_OCCURRENCES = 9;
const int MAX_CONSECUTIVE = 2;
const int TOTAL_DIGITS = 101;
const int HALF_LENGTH = TOTAL_DIGITS / 2;
Random random = new Random();
List<int> requiredDigits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
List<string> listOfCorrectNumbers = new List<string>();

while (true)
{
    List<int> digits = GeneratePalindromeDigits(HALF_LENGTH, MIN_OCCURRENCES, MAX_CONSECUTIVE, requiredDigits, random, TOTAL_DIGITS);

    // Convertendo a lista de dígitos em número
    StringBuilder numberStringBuilder = new StringBuilder();
    foreach (var digit in digits)
    {
        numberStringBuilder.Append(digit);
    }

    string numberString = numberStringBuilder.ToString();
    BigInteger number = BigInteger.Parse(numberString);

    // Verificar divisibilidade
    if (number % 7 == 0 && number % 11 == 0 && number % 73 == 0)
    {
        if (Test.Validate(number.ToString()))
        {
            Console.WriteLine(number.ToString());
            listOfCorrectNumbers.Add(number.ToString());

            if (listOfCorrectNumbers.Count == 40)
                break;
        }
    }
}

static List<int> GeneratePalindromeDigits(int halfLength, int minOccurrences, int maxConsecutive, List<int> requiredDigits, Random random, int totalDigits)
{
    List<int> digits = new List<int>();
    Dictionary<int, int> digitCounts = requiredDigits.ToDictionary(d => d, d => 0);

    // Primeiro, preenchemos a primeira metade garantindo a contagem mínima de cada dígito
    while (digits.Count < halfLength)
    {
        int digit = random.Next(0, 10);

        if (digitCounts[digit] < minOccurrences && (digits.Count < 2 || digit != digits[^1] || digit != digits[^2]))
        {
            digits.Add(digit);
            digitCounts[digit]++;
        }
    }

    // Verificar se todos os dígitos aparecem pelo menos minOccurrences vezes
    foreach (var digit in requiredDigits)
    {
        while (digitCounts[digit] < minOccurrences / 2 + 1)
        {
            int index = random.Next(0, halfLength);
            if (digitCounts[digits[index]] > minOccurrences / 2 + 1)
            {
                digitCounts[digits[index]]--;
                digits[index] = digit;
                digitCounts[digit]++;
            }
        }
    }

    // Espelhando para formar o número capícua
    List<int> fullDigits = new List<int>(digits);
    fullDigits.AddRange(digits.AsEnumerable().Reverse());

    // Garantir os dígitos no meio se o comprimento total for ímpar
    if (totalDigits % 2 != 0)
    {
        int midDigit = random.Next(0, 10);
        fullDigits.Insert(halfLength, midDigit);
        digitCounts[midDigit]++;
    }

    // Ajustar a contagem para garantir que nenhum dígito apareça mais que maxConsecutive vezes seguidas
    AdjustConsecutiveDigits(fullDigits, maxConsecutive, random);

    return fullDigits;
}

static void AdjustConsecutiveDigits(List<int> digits, int maxConsecutive, Random random)
{
    for (int i = 2; i < digits.Count; i++)
    {
        if (digits[i] == digits[i - 1] && digits[i] == digits[i - 2])
        {
            int newDigit;
            do
            {
                newDigit = random.Next(0, 10);
            } while (newDigit == digits[i]);

            digits[i] = newDigit;
        }
    }
}