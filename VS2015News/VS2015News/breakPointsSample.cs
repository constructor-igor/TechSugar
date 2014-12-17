namespace MyName {  //TODO breakpoints sampples
    public class BreakPointsSample {
        static bool MyProperty {
            get; // line 4
            set; // line 5
        }
        public static void ProgramMain() {
            if ((new System.Random()).Next(3) == 1)
                MyProperty = true; // line 9
            else
                MyProperty = false; // line 11
            System.Diagnostics.Debugger.Break(); // line 12
        }
    }
}