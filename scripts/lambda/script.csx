class Apple{
    public string Color {get; set;}

    public int Weight {get; set;}
}

 List<Apple> apples = new List<Apple>{
    new Apple {Color = "Red", Weight = 180},
    new Apple {Color = "Green", Weight = 120},
    new Apple {Color = "Red", Weight = 130},
    new Apple {Color = "Yellow", Weight = 160},
};

IEnumerable<string> redApples = apples
                                .Where(apple => apple.Weight == 180)
                                .Select(apple => apple.Color);


