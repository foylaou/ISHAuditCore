function ConditionalExpression(a1, b1, Expression) {
    var a = parseFloat(a1);
    var b = parseFloat(b1);
    if (isNaN(a1))
        return false;
    if (Expression == "=" && (a == b)) {
        return true;
    } else if (Expression == ">" && (a > b)) {
        return true;
    } else if (Expression == ">=" && (a >= b)) {
        return true;
    } else if (Expression == "<" && (a < b)) {
        return true;
    } else if (Expression == "<=" && (a <= b)) {
        return true;
    }
    return false;
}