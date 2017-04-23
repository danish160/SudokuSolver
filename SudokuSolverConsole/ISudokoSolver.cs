namespace SudokuSolverConsole
{
    public interface ISudokoSolver
    {
        void SolveBoard();
        SudokoBoard GetBoard();
    }
}