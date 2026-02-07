using System.ComponentModel.DataAnnotations;
using System.Text;

public class TextSimilarity
{
    [Required(ErrorMessage = "Введите первый текст!")]
    public string EtalonText {get;set;}

    [Required(ErrorMessage = "Введите второй текст!")]
    public string CopyText{get;set;}

    public string GetSimilarity()
    {
        StringBuilder res = new StringBuilder("");

        char[] delimiters = { ',', ';', ':', ' ', '!' };

        string[] etalonWords = EtalonText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        string[] copyWords = CopyText.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        int[,] matrix = new int[etalonWords.Length + 1, copyWords.Length + 1];

        Stack<int> etalonIndexes = new Stack<int>();

        for (int i = 0; i < etalonWords.Length + 1; i++)
        {
            matrix[i, 0] = 0;
        }

        for (int i = 0; i < copyWords.Length + 1; i++)
        {
            matrix[0, i] = 0;
        }

        for (int i = 1; i < etalonWords.Length + 1; i++)
        {
            for (int j = 1; j < etalonWords.Length + 1; j++)
            {
                if (etalonWords[i - 1] == copyWords[j - 1])
                {
                    matrix[i, j] = matrix[i - 1, j - 1] + 1;
                }
                else
                {
                    matrix[i, j] = Math.Max(matrix[i - 1, j], matrix[i, j - 1]);
                }
            }
        }

        for (int i = etalonWords.Length; i > 0; i--)
        {
            for (int j = copyWords.Length; j > 0; j--)
            {
                if (etalonWords[i - 1] == copyWords[j - 1])
                {
                    etalonIndexes.Push(i - 1);
                }
            }
        }

        while (etalonIndexes.Count > 0)
        {
            res.Append(etalonWords[etalonIndexes.Pop()] + " ");
        }
        return res.ToString();
    }
}