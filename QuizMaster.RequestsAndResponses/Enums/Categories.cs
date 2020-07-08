using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QuizMaster.RequestsAndResponses.Enums
{
    public enum Categories
    {
        [Description("Any Category")]
        AnyCategory = 0,
        [Description("General Knowledge")]
        GeneralKnowledge = 9,
        [Description("Entertainment: Books")]
        Books = 10,
        [Description("Entertainment: Film")]
        Film = 11,
        [Description("Entertainment: Music")]
        Music = 12,
        [Description("Entertainment: Musicals & Theatres")]
        MusicalsAndTheatres = 13,
        [Description("Entertainment: Television")]
        Television = 14,
        [Description("Entertainment: Video Games")]
        VideoGames = 15,
        [Description("Entertainment: Board Games")]
        BoardGames = 16,
        [Description("Science & Nature")]
        ScienceAndNature = 17,
        [Description("Science: Computers")]
        Computers = 18,
        [Description("Science: Mathematics")]
        Mathematics = 19,
        [Description("Mythology")]
        Mythology = 20,
        [Description("Sports")]
        Sports = 21,
        [Description("Geography")]
        Geography = 22,
        [Description("History")]
        History = 23,
        [Description("Politics")]
        Politics = 24,
        [Description("Art")]
        Art = 25,
        [Description("Celebrities")]
        Celebrities = 26,
        [Description("Animals")]
        Animals = 27,
        [Description("Vehicles")]
        Vehicles = 28,
        [Description("Entertainment: Comics")]
        Comics = 29,
        [Description("Science: Gadgets")]
        Gadgets = 30,
        [Description("Entertainment: Japanese Anime & Manga")]
        AnimeAndManga = 31,
        [Description("Entertainment: Cartoon & Animations")]
        CartoonAndAnimations = 32
    }
}
