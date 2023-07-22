namespace TeamsLeague.DAL.Constants.Match.GamePerson
{
    public static class PersonDescriptions
    {
        private const string Aurelia = "Aurelia was born from somewhere between the worlds, studied in the wings of ancient butterflies. Her fate is connected with maintaining the balance between the worlds.";
        private const string Ragnus = "Ragnus was resurrected by magic from hellfire, and now serves as a polite protector of all living against the threat of demons.";
        private const string Ivy = "Ivy is a guardian of nature, assigned to protect the forests from the threat that comes from the world.";
        private const string Cogsworth = "Cogsworth was created by a scientist-mage who combined steampunk technology with a magical essence. He seeks to maintain the balance between the worlds and to develop technologically.";
        private const string Elara = "Elara is the last guard wall of the Ice Mountains and seeks to save this ancient magical land from pernicious threats.";
        private const string Thundar = "As a small boy, Thundar accidentally discovered his magical powers related to thunder. He swore to use them to protect peace and justice.";
        private const string Morok = "Morok was created by ancient mages to protect ancient shrines. But the time of magic has stolen a part of his consciousness, and he is looking for a way to return to his creators.";
        private const string Lyra = "Lyra is an eccentric magician-illusionist who became famous for her unusual tricks and ability to transform reality.";
        private const string Grimclaw = "Grimclaw was once an ordinary tiger who accidentally fell victim to magic, turning into a half-human. He is looking for a way to return to his original form.";
        private const string Nymphia = "Nymphia is the guardian of the deep sea and the magical water world. She strives to save her home from the dangers.";
        private const string Ignis = "Ignis, was once an ordinary demon, but gained the power of fire, which changed his life forever. He now devotes his whole life to spreading hellfire.";
        private const string Vera = "Vera lives apart from world, she has dedicated her life to studying wind magic and protecting wildlife.";
        private const string Zephyr = "Zephyr is a small mountain beast that gained the power of the wind and became the guardian of mountains and ridges.";
        private const string Solus = "Solus is the last master of a magical order that must preserve the knowledge of ancient times. He dreams of teaching a new generation of mages and inheritors of his wisdom.";
        private const string Grom = "Grom was created by ancient mages to protect the city from attacks. Now he has left his previous destination and is giving himself up to his own desires.";
        private const string Vespera = "Vespera lives between the worlds of light and darkness, trying to keep the balance between them and prevent the light from disappearing completely.";
        private const string Zarael = "Zarael belongs to an ancient order of elven archers who have preserved the traditions and art of archery throughout the centuries.";
        private const string Tusk = "Tusk is a loyal protector of his tribe and devoted to the traditions of his people. He stands guard over the land of his ancestors.";
        private const string Lunara = "Lunara studied forbidden magical knowledge and became a mage of lunar powers. She seeks to control magic and protect the world from its abuse.";
        private const string Hex = "Hex is a mysterious magical creature that emerged from the darkness. He has his own goals and reasons, not knowing whether he will act for good or evil.";
        private const string Dracor = "Dracor is one of the few great dragons who fights to preserve his race and a magical realm that suggests an eternal source of power.";
        private const string Arachna = "Arachna is a magical spider that permeates with its mysterious character. She leaves secrets and paths in her path.";
        private const string Zilon = "Zilon is the last of the mystical dragon men to retain all the ancient lore of his race. He is looking for a way to preserve traditions and ensure peace.";
        private const string Lyth = "Lyth is a mysterious armorer who has studied armorial magic for centuries. He preserves ancient knowledge and seeks to control magic for the good of the world.";

        public static string GetDescription(GamePersonType type) => type switch
        {
            GamePersonType.Aurelia => Aurelia,
            GamePersonType.Ragnus => Ragnus,
            GamePersonType.Ivy => Ivy,
            GamePersonType.Cogsworth => Cogsworth,
            GamePersonType.Elara => Elara,
            GamePersonType.Thundar => Thundar,
            GamePersonType.Morok => Morok,
            GamePersonType.Lyra => Lyra,
            GamePersonType.Grimclaw => Grimclaw,
            GamePersonType.Nymphia => Nymphia,
            GamePersonType.Ignis => Ignis,
            GamePersonType.Vera => Vera,
            GamePersonType.Zephyr => Zephyr,
            GamePersonType.Solus => Solus,
            GamePersonType.Grom => Grom,
            GamePersonType.Vespera => Vespera,
            GamePersonType.Zarael => Zarael,
            GamePersonType.Tusk => Tusk,
            GamePersonType.Lunara => Lunara,
            GamePersonType.Hex => Hex,
            GamePersonType.Dracor => Dracor,
            GamePersonType.Arachna => Arachna,
            GamePersonType.Zilon => Zilon,
            GamePersonType.Lyth => Lyth,
            _ => throw new NotImplementedException(),
        };
    }
}
