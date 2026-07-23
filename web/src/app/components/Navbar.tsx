import Link from 'next/link'
import React from 'react'
import { RiNextjsFill } from 'react-icons/ri'

const Navbar = () => {
    return (
        <nav className="fixed top-6 left-1/2 -translate-x-1/2 z-50 justify-center">
            <div className="border-b-2 border-gray-700 px-8 py-3">
                <ul className="flex items-center gap-10 text-lg text-black">
                    <li>
                        <Link href="/" className="hover:text-gray-300 transition-colors">
                        Race
                        </Link>
                    </li>
                    <li>
                        <Link href="/chickens" className="hover:text-gray-300 transition-colors">
                        Chickens
                        </Link>
                    </li>
                    <li>
                        <Link href="/about" className="hover:text-gray-300 transition-colors">
                        About
                        </Link>
                    </li>
                </ul>
            </div>
        </nav>
    )
}

export default Navbar