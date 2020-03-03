export class ExpressionAppVariants {
    // Expression invariants
    public static readonly AppMailExpression = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$/;
    public static readonly AppNumberExpression = /^\d+$/;
    public static readonly AppInfiniteDecimalExpression = /^[0-9]*\.?[0-9]*$/g;
    public static readonly AppTwoDecimalExpression = /^[0-9]*\.?[0-9]{0,2}$/g;
}
