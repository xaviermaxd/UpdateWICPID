namespace Common.Http
{
    public class EResponseBase<TEntity> : ICloneable where TEntity : class, new()
    {
        public int Code { get; set; }
        public string MessageES { get; set; } = string.Empty;
        public string MessageEN { get; set; } = string.Empty;
        public bool IsResultList { get; set; } = false;
        public ICollection<TEntity>? List { get; set; }
        public TEntity? Object { get; set; }
        public string? Data { get; set; }
        public HashSet<string>? FunctionalErrors { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return $"Response[Code: {Code}, Message: {MessageES},  listado: {List} , objeto {Object}]";
        }

    }
}
