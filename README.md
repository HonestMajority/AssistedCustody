# Assisted Bitcoin Custody Prototype

This prototype demonstrates a collaborative custody scheme for Bitcoin where a trusted server assists users while preserving their ability to exit on their own.

## Goals
- **Unilateral Exit**: Customers must always be able to withdraw their funds independently after a delay.
- **Seamless UX for Happy Path**: Normal withdrawals are collaborative and require minimal user interaction.
- **Error Recovery**: The server can help recover from a single critical user error, such as a lost or compromised key.

## Architecture & Flow
### Key Generation
- The client generates a mnemonic phrase and key.
- The server generates its own secure key and receives the client’s extended public key.

### Holding Account
- A 2-of-2 multisig P2WPKH address is created from the client and server keys and serves as the holding account.

### Exit Transaction
- When funds are deposited, the server and client collaboratively create but do not broadcast an exit transaction draining all funds to the holding account script.

### Holding Account Script
The holding account supports three spending paths:
1. **Collaborative Withdrawal (Immediate)** – both client and server sign to move funds.
2. **User Unilateral Exit (After 3 Months)** – the client can sweep funds alone after a delay.
3. **Server Recovery (After 6 Months)** – the server can sweep funds to help recover the user’s funds.

## Building
This project targets **.NET 9.0 preview**.

```bash
# restore dependencies and compile
cd /workspace/AssistedCustody
dotnet build

# run the web application
 dotnet run
```

If using containers, a `Dockerfile` and `compose.yaml` are provided for local development.

## Disclaimer
This is experimental software intended for demonstration and education. It has not been reviewed for production use.
