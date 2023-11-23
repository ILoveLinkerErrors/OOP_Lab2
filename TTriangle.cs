class TTRiangle {
    // 4. Клас “Трикутник ” – TTriangle
    // поля:  для зберігання довжин сторін;
    // методи:
    //  конструктор без параметрів, конструктор з параметрами, конструктор
    // копіювання;
    //  введення/виведення даних;
    //  визначення площі;
    //  визначення периметру;
    //  порівняння з іншим трикутником;
    //  перевантаження операторів + (додавання довжин сторін), – (віднімання
    // довжин відповідних сторін), * (множення сторін на деяке число).
    private double sideA;
    private double sideB;
    private double sideC;

    private bool isInitialized = false;

    private static readonly double EPSILON = 1e-8;
    
    public double SideA
    {
        get {
            return sideA; 
        }
        set { 
            if (value < EPSILON) {
                Console.Error.WriteLine("Fatal Error: triangle side <A> must be bigger than 0.");
                Environment.Exit(1);
            }
            if (isInitialized) {
                if (!IsValidTriangle(value, sideB, sideC)) {
                    Console.Error.WriteLine("Fatal Error: current lengths of sides <B>, <C> and {0} do not form a valid triangle.", value);
                    Environment.Exit(1);
                }
            }
            sideA = value; 
        }
    }

    public double SideB
    {
        get { 
            return sideB; 
        }
        set {
            if (value < EPSILON) {
                Console.Error.WriteLine("Fatal Error: triangle side <B> must be bigger than 0.");
                Environment.Exit(1);
            }
            if (isInitialized) {
                if (!IsValidTriangle(SideA, value, sideC)) {
                    Console.Error.WriteLine("Fatal Error: current lengths of sides <A>, <C> and {0} do not form a valid triangle.", value);
                    Environment.Exit(1);
                }
            }
            sideB = value; 
        }
    }

    public double SideC
    {
        get { 
            return sideC; 
        }
        set { 
            if (value < EPSILON) {
                Console.Error.WriteLine("Fatal Error: triangle side <C> must be bigger than 0.");
                Environment.Exit(1);
            }
            if (isInitialized) {
                if (!IsValidTriangle(SideA, SideB, value)) {
                    Console.Error.WriteLine("Fatal Error: current lengths of sides <A>, <B> and {0} do not form a valid triangle.", value);
                    Environment.Exit(1);
                }
            }
            sideC = value; 
        }
    }

    private static bool IsValidTriangle(double a, double b, double c) {
        if (a < EPSILON || b < EPSILON || c < EPSILON) {
            return false;
        }
        return a + b - c > EPSILON && a + c - b > EPSILON && b + c - a > EPSILON;
    }

    public TTRiangle(double A, double B, double C) {  
        if (!IsValidTriangle(A, B, C)) {
            Console.Error.WriteLine("Fatal Error: sides {0}, {1}, {2} do not form a valid triangle.", A, B, C);
            Environment.Exit(1);
        }   
        sideA = A;
        sideB = B;
        sideC = C;
        isInitialized = true;
    }

    public TTRiangle() {
        SideA = 1;
        SideB = 1;
        SideC = 1;
        isInitialized = true;
    }

    public void Input() {
        static double getSide(string sideName)
        {
            while (true)
            {
                Console.Write("Side<{0}> = ", sideName);
                var strSideLen = Console.ReadLine();
                double SideLen;
                try
                {
                    SideLen = double.Parse(strSideLen);
                }
                catch
                {
                    Console.WriteLine("Error: couldn't convert your input <\"{0}\"> to double, try again.", strSideLen);
                    continue;
                }
                if (SideLen < EPSILON)
                {
                    Console.WriteLine("Error: side length must be bigger than 0, try again.");
                    continue;
                }
                return (double)SideLen;
            }
        }
        double a =  getSide("A");
        double b =  getSide("B");
        double c =  getSide("C");
        if (!IsValidTriangle(a, b, c)) {
            Console.Error.WriteLine("Fatal Error: sides {0}, {1}, {2} do not form a valid triangle.", a, b, c);
            Environment.Exit(1);
        } 
        sideA = a;
        sideB = b;
        sideC = c;
    }

    public void Print() {
        Console.WriteLine("\nTriangle sides: ");
        Console.WriteLine("Side<A> = {0}", SideA);
        Console.WriteLine("Side<B> = {0}", SideB);
        Console.WriteLine("Side<C> = {0}", SideC);
    }

    public double GetPerimeter() {
        return SideA + SideB + SideC;
    }

    public double GetArea() {
        double halfPerim = 0.5 * GetPerimeter();
        return Math.Sqrt(halfPerim * (halfPerim - SideA) * (halfPerim - SideB) * (halfPerim - SideC));
    }
    
    public static TTRiangle operator +(TTRiangle t1, TTRiangle t2) {
        double newA = t1.SideA + t2.SideA;
        double newB = t1.SideB + t2.SideB;
        double newC = t1.SideC + t2.SideC;
        if (!IsValidTriangle(newA, newB, newC)) {
            Console.Error.WriteLine("Fatal Error: sides of the new triangle (result of summation): {0}, {1}, {2} - are not valid.", newA, newB, newC);
            Environment.Exit(1);
        }       
        return new TTRiangle(newA, newB, newC);
    }

    public static TTRiangle operator -(TTRiangle t1, TTRiangle t2) {
        double newA = t1.SideA - t2.SideA;
        double newB = t1.SideB - t2.SideB;
        double newC = t1.SideC - t2.SideC;
        if (!IsValidTriangle(newA, newB, newC)) {
            Console.Error.WriteLine("Fatal Error: sides of the new triangle (result of subtraction): {0}, {1}, {2} - are not valid.", newA, newB, newC);
            Environment.Exit(1);
        } 
        return new TTRiangle(newA, newB, newC);
    }

    public static TTRiangle operator *(TTRiangle op1, double op2) {
        if (op2 < EPSILON) {
            Console.Error.WriteLine("Fatal Error: can't multiply triangle by a scalar less than zero");
            Environment.Exit(1);
        }
        return new TTRiangle(op1.SideA * op2, op1.SideB * op2, op1.SideC * op2);
    }

    public static TTRiangle operator *(double op1, TTRiangle op2) {
        return op2 * op1;
    }

    public static bool operator == (TTRiangle t1, TTRiangle t2) {
        return Math.Abs(t1.SideA - t2.SideA) < EPSILON && Math.Abs(t1.SideB - t2.SideB) < EPSILON && Math.Abs(t1.SideC - t2.SideC) < EPSILON;
    }

    public static bool operator != (TTRiangle t1, TTRiangle t2) {
        return !(t1 == t2);
    }

    public static bool operator > (TTRiangle t1, TTRiangle t2) {
        return t1.SideA - t2.SideA > EPSILON && t1.SideB - t2.SideB > EPSILON && t1.SideC - t2.SideC > EPSILON; 
    }

    public static bool operator < (TTRiangle t1, TTRiangle t2) {
        return t2.SideA - t1.SideA > EPSILON && t2.SideB - t1.SideB > EPSILON && t2.SideC - t1.SideC > EPSILON; 
    }
}