﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LanguageExt.TypeClasses
{
    /// <summary>
    /// Monad type-class
    /// </summary>
    /// <typeparam name="A">Bound value</typeparam>
    [Typeclass]
    public interface Monad<MA, A> : Foldable<MA, A>
    {
        /// <summary>
        /// Monadic bind
        /// </summary>
        /// <typeparam name="MONADB">Type-class of the return value</typeparam>
        /// <typeparam name="MB">Type of the monad to return</typeparam>
        /// <typeparam name="B">Type of the bound return value</typeparam>
        /// <param name="ma">Monad to bind</param>
        /// <param name="f">Bind function</param>
        /// <returns>Monad of type MB derived from Monad of B</returns>
        [Pure]
        MB Bind<MONADB, MB, B>(MA ma, Func<A, MB> f) where MONADB : struct, Monad<MB, B>;

        /// <summary>
        /// Monad return
        /// </summary>
        /// <typeparam name="A">Type of the bound monad value</typeparam>
        /// <param name="x">The bound monad value</param>
        /// <returns>Monad of A</returns>
        [Pure]
        MA Return(A x);

        /// <summary>
        /// Monad return
        /// </summary>
        /// <param name="f">The function to invoke to get the bound monad value(s)</param>
        /// <returns>Monad of A</returns>
        [Pure]
        MA Return(Func<A> f);

        /// <summary>
        /// Monad return
        /// </summary>
        /// <param name="xs">The bound monad value(s)</param>
        /// <returns>Monad of A</returns>
        [Pure]
        MA FromSeq(IEnumerable<A> xs);

        /// <summary>
        /// Produce a failure value
        /// </summary>
        [Pure]
        MA Fail(Exception err = null);

        /// <summary>
        /// Produce a failure value
        /// </summary>
        [Pure]
        MA Fail(object err);
    }

    /// <summary>
    /// Monad type-class
    /// </summary>
    /// <typeparam name="A">Bound value</typeparam>
    [Typeclass]
    public interface Monad<Env, MA, A> 
    {
        /// <summary>
        /// Monadic bind
        /// </summary>
        /// <typeparam name="MONADB">Type-class of the return value</typeparam>
        /// <typeparam name="MB">Type of the monad to return</typeparam>
        /// <typeparam name="B">Type of the bound return value</typeparam>
        /// <param name="ma">Monad to bind</param>
        /// <param name="f">Bind function</param>
        /// <returns>Monad of type MB derived from Monad of B</returns>
        [Pure]
        MB Bind<MONADB, MB, B>(MA ma, Func<A, MB> f) 
            where MONADB : struct, Monad<Env, MB, B>;

        /// <summary>
        /// Monad return
        /// </summary>
        /// <typeparam name="A">Type of the bound monad value</typeparam>
        /// <param name="x">The bound monad value</param>
        /// <returns>Monad of A</returns>
        [Pure]
        MA Return(A x);

        /// <summary>
        /// Monad return
        /// </summary>
        /// <param name="f">The function to invoke to get the bound monad value(s)</param>
        /// <returns>Monad of A</returns>
        [Pure]
        MA Return(Func<Env, (A, Env, bool)> f);

        /// <summary>
        /// Monad run
        /// </summary>
        /// <param name="f">The function to invoke to get the monad value(s)</param>
        /// <returns>Monad of A</returns>
        [Pure]
        (A, Env, bool) Eval(MA ma, Env e);

        /// <summary>
        /// Produce a failure value
        /// </summary>
        [Pure]
        MA Fail(Exception err = null);

        /// <summary>
        /// Produce a failure value
        /// </summary>
        [Pure]
        MA Fail(object err);
    }
}