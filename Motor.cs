internal class Motor
{
    private string _motor;

    public string command
    {
        get { return _motor; }
        set
        {
            // Converteer "vooruit" naar "a"
            if (value == "vooruit")
            {
                _motor = "a";
            }
            else if (value == "1" || value == "2" || value == "3" || value == "4" || value == "a")
            {
                _motor = value;
            }
            else
            {
                throw new ArgumentException("Invalid motor value. Valid values are 1, 2, 3, 4, and vooruit.");
            }
        }
    }
}
