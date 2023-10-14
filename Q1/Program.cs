string text = @"Even though apple is not an excellent source of dietary fiber (it ranks as a ""good"" source in our WHFoods Rating System), the fiber found in apple may combine with other apple nutrients to provide you with the kind of health benefits you would ordinarily only associate with much higher amounts of dietary fiber. These health benefits are particularly important in prevention of heart disease through healthy regulation of blood fat levels. Recent research has shown that intake of apples in their whole food form can significantly lower many of our blood fats.";

string keyword = "apple";
int keywordLength = keyword.Length;
int textLength = text.Length;

for (int i = 0; i <= textLength - keywordLength; i++)
{
    if (text[i] == 'a' && text.Substring(i, keywordLength) == keyword)
    {
        Console.WriteLine($"Found 'apple' at index {i}");
    }
}