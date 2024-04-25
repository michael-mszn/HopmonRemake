namespace Setup
{
    /*
     * UIManager will wait for all scripts implementing this interface
     * to set the isInitialized to true. This ensures that UI values
     * properly get reloaded upon restart and separation of concern as
     * other classes shouldn't be responsible to Initialize the UI.
     */
    public interface IInitializedFlag
    {
        public bool IsInitialized();
    }
}