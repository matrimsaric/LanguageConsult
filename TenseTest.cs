using LanguageConsult;
using LanguageConsult.Verbs;

namespace ConsultLibraryTests
{
    [TestClass]
    public class TenseTest
    {
        private Guid guidToUse = Guid.NewGuid();
        #region Empty Tests
        [TestMethod]
        public void BlankKanji()
        {
            try
            {
                Tense tense = new Tense("", "a", "b", "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Kanji was blank exception should have been thrown");



        }

        [TestMethod]
        public void BlankHiragana()
        {
            try
            {
                Tense tense = new Tense("a", "", "b", "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Hiragana was blank exception should have been thrown");



        }

        [TestMethod]
        public void BlankRomaji()
        {
            try
            {
                Tense tense = new Tense("a", "a", "", "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Romaji was blank exception should have been thrown");



        }

        [TestMethod]
        public void BlankMeaning()
        {
            try
            {
                Tense tense = new Tense("a", "a", "b", "", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Meaning was blank exception should have been thrown");



        }

        [TestMethod]
        public void EmptyGuid()
        {
            try
            {
                Tense tense = new Tense("a", "a", "b", "", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("GUID was empty exception should have been thrown");



        }
        #endregion Empty Tests


        #region Hack Tests
        private string hackText = @"<img src=x onerror=&#x22;&#x61;&#x6C;&#x65;&#x72;&#x74;&#x28;&#x31;&#x29;&#x22;>";

        [TestMethod]
        public void HackKanji()
        {
            try
            {
                Tense tense = new Tense(hackText, "a", "b", "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Kanji was illegal exception should have been thrown");



        }

        [TestMethod]
        public void HackHiragana()
        {
            try
            {
                Tense tense = new Tense("a", hackText, "b", "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Hiragana was illegal exception should have been thrown");



        }

        [TestMethod]
        public void HackRomaji()
        {
            try
            {
                Tense tense = new Tense("a", "v", hackText, "c", "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Romaji was illegal exception should have been thrown");



        }

        [TestMethod]
        public void HackMeaning()
        {
            try
            {
                Tense tense = new Tense("a", "v", "b", hackText, "d", guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Meaning was illegal exception should have been thrown");



        }

        [TestMethod]
        public void HackNotes()
        {
            try
            {
                Tense tense = new Tense("a", "v", "b", "c", hackText, guidToUse, TENSE_TYPE.OTHER);
            }
            catch
            {
                return;
            }

            Assert.Fail("Notes was illegal exception should have been thrown");



        }
        #endregion Hack Tests

        #region Valid tests
        [TestMethod]
        public void ValidEntry()
        {
            try
            {
                Tense tense = new Tense("書く", "かく", "Kaku", "To Write", "", guidToUse, TENSE_TYPE.PAST_POSITIVE);
            }
            catch
            {
                Assert.Fail("Entries were valid - Exception should not have been thrown");
            }

            return;

            



        }
        #endregion Valid tests


    }
}