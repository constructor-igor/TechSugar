DATE: 01-10-2014

CHANGES:
Accounts SDK:
1. Added TotalCash (BigDecimal) field in Balance class. This class is a member of AccountBalanceResponse. This change affects the AccountBalance api response.
2. Added currentPrice (BigDecimal) in AccountPosition class. This class is a member of AccountPositionsResponse. This change affects the AccountPosition api response.
3. Added support for transactionHistory apis:
	1. Added new classes:
		Transaction.java
		TransactionGroupingEnum.java
		TransactionRequest.java
		TransactionRequestEnum.java
		TransactionTypeEnum.java
		TransactionTypeTrades.java
		TransactionUtils.java
		TransactionValidator.java
		GetTransactionDetailsResponse.java
		GetTransactionHistoryResponse.java
	2. Added new functions to handle various scenarios of getting transaction history details
		getTransactionHistory()
		getTransactionDetails()
		getAllTransactions()
		getAllTransactions() //Overloaded function
		getTransactionsFor()
		getTransactionDetails //Overlaoded function
		getTransactions()
		getTrades()
		getDeposits()
		getWithdrawals()
		getDepositsOrWithdrawals()
		

Order SDK : No Change

Market SDK : No Change

OAuth SDK : No Change
