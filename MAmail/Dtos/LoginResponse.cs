namespace MAmail.Dtos
{
    public struct LoginResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
    }
}
