namespace PlainBB84Protocol {

    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Canon;
    open Microsoft.Quantum.Intrinsic;

    operation GetRandomBoolean() : Bool
    {
        use q = Qubit();
        H(q);
        return ResultAsBool(M(q));
    }
    
    operation GetOneRunResults() : (Bool, Bool, Bool, Bool) 
    {
        // Generate both Alice's bit and Alice's basis randomly.
        // Allocate a qubit and prepare it in the right basis:
        // |0⟩ and |1⟩ for the rectilinear basis, |+⟩ and |-⟩ for the diagonal basis.
        use q = Qubit();
        let aliceBase = GetRandomBoolean();

        if aliceBase
        {
            H(q);
        }

        let aliceBit = GetRandomBoolean();
        if aliceBit // flip the qubit at to '1'
        {
            X(q);
        }

        // Send the qubit over to Bob, who generates his basis randomly...
        // ...and measures the qubit in that basis
        let bobBase = GetRandomBoolean();
        if bobBase
        {
            H(q);
        }

        // We know the protocol succeeds if Alice and Bob chose the same bases;
        // otherwise, just discard the run.
    
        let bobBit = ResultAsBool(M(q));

        return (aliceBase, aliceBit, bobBase, bobBit);
    }

    operation RunBB84Protocol(numberOfBits : Int) : (Bool, Bool, Bool, Bool)[] {
        Message("Running BB84 protocol...");
        
        mutable output = new (Bool, Bool, Bool, Bool)[0];
        
        for i in 1 .. numberOfBits
        {
            set output += [GetOneRunResults()];
        }
        
        return output;
	}
}