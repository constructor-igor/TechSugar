//
// https://stackoverflow.com/questions/667802/what-is-the-algorithm-to-convert-an-excel-column-letter-into-its-number
//
function ExcelColumnNameToNumber(columnName) {
    if (!columnName){
        throw "Empty argument"
    }
    var columnName = columnName.toUpperCase();

    var sum = 0;
    for (i = 0; i < columnName.length; i++) {
        sum *= 26;
        var current_char = columnName[i].charCodeAt(0);
        sum += (current_char - 'A'.charCodeAt(0) + 1);
    }

    return sum;
}

console.log("start")

index_of_column_A = ExcelColumnNameToNumber("A")
console.log("index_of_column_A: ", index_of_column_A)

index_of_column_AH = ExcelColumnNameToNumber("AH")
console.log("index_of_column_AH: ", index_of_column_AH)

index_of_column_XFD = ExcelColumnNameToNumber("XFD")
console.log("index_of_column_XFD: ", index_of_column_XFD)


// ExcelColumnNameToNumber("")

console.log("finish")